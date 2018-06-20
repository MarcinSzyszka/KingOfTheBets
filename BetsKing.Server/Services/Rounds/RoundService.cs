using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
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
