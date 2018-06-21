using AutoMapper;
using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;
using BetsKing.Shared.ViewModels.Rounds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsKing.Server.Services.Rounds
{
    public class RoundService : IRoundService
    {
        readonly BetsKingDbContext _dbContext;

        public RoundService(BetsKingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Round>> GetAll(int tournamentId)
        {
            return await _dbContext.Rounds.Include(r => r.Tournament)
                                    .Include(r => r.Matches)
                                    .Where(r => r.TournamentId == tournamentId)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<RoundWithBetsViewModel>> GetByTournamentAndGambler(int tournamentId, int gamblerId)
        {
            var rounds = await _dbContext.Rounds.Include(r => r.Tournament).ThenInclude(t => t.Gamblers)
                                        .Where(r => r.TournamentId == tournamentId && r.Tournament.Gamblers.Any(g => g.GamblerId == gamblerId && g.IsActive))
                                        .ToListAsync();

            var results = new List<RoundWithBetsViewModel>(rounds.Count);

            var roundsId = rounds.Select(r => r.Id);

            var gamblerBets = await _dbContext.MatchBets.Include(m => m.Match)
                                                 .Where(m => m.GamblerId == gamblerId && roundsId.Any(r => r == m.Match.RoundId))
                                                 .ToListAsync();

            foreach (var round in rounds)
            {
                var roundVm = Mapper.Map<Round, RoundWithBetsViewModel>(round);

                roundVm.MatchBets =  Mapper.Map<IEnumerable<MatchBet>, IEnumerable<MatchBetViewModel>>(gamblerBets.Where(b => b.Match.RoundId == round.Id));

                results.Add(roundVm);
            }

            return results;
        }

        public Task<Round> Get(int id)
        {
            return _dbContext.Rounds.Include(r => r.Tournament)
                                    .Include(r => r.Matches)
                                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Round> Create(string name, int tournamentId)
        {
            var round = await _dbContext.Rounds.FirstOrDefaultAsync(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && t.TournamentId == tournamentId);

            if (round == null)
            {
                round = new Round { Name = name, TournamentId = tournamentId };

                await _dbContext.Rounds.AddAsync(round);

                await _dbContext.SaveChangesAsync();
            }

            return round;
        }
    }
}
