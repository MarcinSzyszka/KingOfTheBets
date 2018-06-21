using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsKing.Server.Services.Matches
{
    public class MatchBetService : IMatchBetService
    {
        readonly BetsKingDbContext _dbContext;

        public MatchBetService(BetsKingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddOrUpdateGamblerMatchBets(int tournamentId, int gamlberId)
        {
            var tournamentMatches = await _dbContext.Matches.Include(m => m.Round)
                                                            .Include(m => m.Bets)
                                                            .Where(t => t.Round.TournamentId == tournamentId && t.Bets.All(b => b.GamblerId != gamlberId)).ToListAsync();

            foreach (var match in tournamentMatches)
            {
                await _dbContext.MatchBets.AddAsync(new MatchBet
                {
                    GamblerId = gamlberId,
                    MatchId = match.Id
                });
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<MatchBet>> SaveBets(SaveMatchBetsViewModel model)
        {
            var gamblerBets = await _dbContext.MatchBets.Include(m => m.Match)
                                                        .Where(m => m.GamblerId == model.GamblerId && m.Match.RoundId == model.RoundId).ToListAsync();

            foreach (var bet in model.Bets)
            {
                var betToUpdate = gamblerBets.FirstOrDefault(b => b.Id == bet.Id && !b.TeamAScore.HasValue);

                if (betToUpdate == null)
                {
                    continue;
                }

                betToUpdate.TeamAScore = bet.TeamAScore;
                betToUpdate.TeamBScore = bet.TeamBScore;

                _dbContext.Entry(betToUpdate).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

            return gamblerBets;
        }
    }
}
