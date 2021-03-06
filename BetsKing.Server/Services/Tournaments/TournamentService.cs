﻿using AutoMapper;
using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Extensions;
using BetsKing.Server.Services.Matches;
using BetsKing.Shared.ViewModels.Tournaments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsKing.Server.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        readonly BetsKingDbContext _dbContext;
        readonly IMatchBetService _matchBetService;

        public TournamentService(BetsKingDbContext dbContext, IMatchBetService matchBetService)
        {
            _dbContext = dbContext;
            _matchBetService = matchBetService;
        }

        public IEnumerable<Tournament> GetAll()
        {
            return _dbContext.Tournaments;
        }

        public Task<Tournament> Get(int id)
        {
            return _dbContext.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tournament> Create(string name)
        {
            var tournament = await _dbContext.Tournaments.FirstOrDefaultAsync(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (tournament == null)
            {
                tournament = new Tournament { Name = name };

                await _dbContext.Tournaments.AddAsync(tournament);

                await _dbContext.SaveChangesAsync();
            }

            return tournament;
        }

        public async Task<IEnumerable<TournamentGamblerViewModel>> GetAllGamblers(int tournamentId)
        {
            var gamblers = await _dbContext.Gamblers.Include(g => g.Tournaments).ToListAsync();

            var results = new List<TournamentGamblerViewModel>(gamblers.Count);

            foreach (var gambler in gamblers)
            {
                var tournamnentGambler = Mapper.Map<Gambler, TournamentGamblerViewModel>(gambler);
                tournamnentGambler.TournamentId = tournamentId;
                tournamnentGambler.Participates = gambler.Tournaments.Any(t => t.TournamentId == tournamentId && t.IsActive);

                results.Add(tournamnentGambler);
            }

            return results;
        }

        public async Task<bool> SetGamblers(SetTournamentGamblersViewModel model)
        {
            var tournamentGamblers = await _dbContext.TournamentGamblers.Where(t => t.TournamentId == model.TournamentId).ToListAsync();

            var gamblersToActivate = tournamentGamblers.Where(g => model.GamblersIds.Contains(g.GamblerId) && !g.IsActive).ToList();

            foreach (var gambler in gamblersToActivate)
            {
                gambler.IsActive = true;

                _dbContext.Entry(gambler).State = EntityState.Modified;

                await _matchBetService.AddOrUpdateGamblerMatchBets(model.TournamentId, gambler.GamblerId);
            }

            var gamblersToDeactivate = tournamentGamblers.Where(g => !model.GamblersIds.Contains(g.GamblerId)).ToList();

            foreach (var gambler in gamblersToDeactivate)
            {
                gambler.IsActive = false;

                _dbContext.Entry(gambler).State = EntityState.Modified;
            }

            var gamblersIdsToAdd = model.GamblersIds.Except(tournamentGamblers.Select(g => g.GamblerId));

            foreach (var gamblerId in gamblersIdsToAdd)
            {
                var tournamentGambler = new TournamentGambler
                {
                    GamblerId = gamblerId,
                    TournamentId = model.TournamentId
                };

                _dbContext.TournamentGamblers.Add(tournamentGambler);
                await _matchBetService.AddOrUpdateGamblerMatchBets(model.TournamentId, gamblerId);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Tournament>> GetForGambler(int gamblerId)
        {
            return await _dbContext.Tournaments.Include(t => t.Gamblers).Where(t => t.Gamblers.Any(g => g.GamblerId == gamblerId && g.IsActive)).ToListAsync();
        }

        public async Task<TournamentResultsViewModel> GetResults(int tournamentId)
        {
            var tournament = await _dbContext.Tournaments.Include(t=> t.Gamblers).ThenInclude(g => g.Gambler)
                                                         .Include( t=> t.Rounds).ThenInclude(r => r.Matches).ThenInclude(m => m.Bets)
                                                         .FirstAsync(t => t.Id == tournamentId);

            var result = new TournamentResultsViewModel
            {
                TournamentId = tournament.Id
            };

            var gamblers = tournament.Gamblers.Where(g => g.IsActive).Select(g => g.Gambler).Distinct().ToList();

            result.GamblersResults = new List<TournamentGamblerResultsViewModel>(gamblers.Count);

            foreach (var gambler in gamblers)
            {
                var gamblerResult = new TournamentGamblerResultsViewModel
                {
                    GamblerId = gambler.Id,
                    GamblerDisplayName = gambler.DisplayName,
                    TorunamentId = tournament.Id
                };

                var finishedMatches = tournament.Rounds.SelectMany(r => r.Matches.Where(m => m.GetResult() != Shared.Enums.MatchResultEnum.Pending));

                var gamblerBets = tournament.Rounds.SelectMany(r => r.Matches.SelectMany(m => m.Bets));

                foreach (var match in finishedMatches)
                {
                    var betForMatch = gamblerBets.FirstOrDefault(b => b.MatchId == match.Id);

                    if (betForMatch == null)
                    {
                        //Gambler could not placed the bet for this macth
                        continue;
                    }

                    gamblerResult.PointsForTypingMatchResult += match.GetResult() == betForMatch.GetResult() ? 1 : 0;

                    if (match.TeamAScore == betForMatch.TeamAScore && match.TeamBScore == betForMatch.TeamBScore)
                    {
                        gamblerResult.PointsForTypingExactMatchScore += 3;
                    }
                }

                result.GamblersResults.Add(gamblerResult);
            }

            result.GamblersResults = result.GamblersResults.OrderByDescending(g => g.GeneralPoints).ToList();

            return result;
        }
    }
}
