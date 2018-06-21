using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsKing.Server.Services.Matches
{
    public class MatchService : IMatchService
    {
        readonly BetsKingDbContext _dbContext;
        IMatchBetService _matchBetService;

        public MatchService(BetsKingDbContext dbContext, IMatchBetService matchBetService)
        {
            _dbContext = dbContext;
            _matchBetService = matchBetService;
        }

        public async Task<IEnumerable<Match>> GetAll(int roundId)
        {
            return await _dbContext.Matches.Include(r => r.Round)
                                    .Where(r => r.RoundId == roundId)
                                    .ToListAsync();
        }

        public Task<Match> Get(int id)
        {
            return _dbContext.Matches.Include(r => r.Round)
                                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Match> Create(AddMatchViewModel model)
        {
            var tournament = await _dbContext.Tournaments.Include(t => t.Gamblers).FirstOrDefaultAsync(t => t.Rounds.Any(r => r.Id == model.RoundId));

            var match = await _dbContext.Matches.FirstOrDefaultAsync(t => t.TeamAName.Equals(model.TeamAName, StringComparison.InvariantCultureIgnoreCase) &&
                                                                           t.TeamBName.Equals(model.TeamBName, StringComparison.InvariantCultureIgnoreCase) &&
                                                                           t.RoundId == model.RoundId);

            if (match == null)
            {
                match = new Match
                {
                    TeamAName = model.TeamAName,
                    TeamBName = model.TeamBName,
                    TeamAScore = model.TeamAScore,
                    TeamBScore = model.TeamBScore,
                    RoundId = model.RoundId
                };

                await _dbContext.Matches.AddAsync(match);

                await _dbContext.SaveChangesAsync();

                var activeGamblers = tournament.Gamblers.Where(g => g.IsActive);

                foreach (var gambler in activeGamblers)
                {
                    await _matchBetService.AddOrUpdateGamblerMatchBets(tournament.Id, gambler.GamblerId);
                }
            }

            return match;
        }

        public async Task<bool> UpdateScore(UpdateMatchScoreViewModel model)
        {
            var match = await _dbContext.Matches.FirstOrDefaultAsync(m => m.Id == model.Id);

            if (match == null)
            {
                return false;
            }

            match.TeamAScore = model.TeamAScore;
            match.TeamBScore = model.TeamBScore;

            _dbContext.Entry(match).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
