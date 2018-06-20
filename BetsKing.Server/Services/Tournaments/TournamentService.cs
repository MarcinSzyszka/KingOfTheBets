using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetsKing.Server.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        readonly BetsKingDbContext _dbContext;

        public TournamentService(BetsKingDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
