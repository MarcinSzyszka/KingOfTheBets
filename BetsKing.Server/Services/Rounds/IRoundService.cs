using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;

namespace BetsKing.Server.Services.Rounds
{
    public interface IRoundService
    {
        Task<Round> Create(string name, int tournamentId);
        Task<Round> Get(int id);
        Task<IEnumerable<Round>> GetAll(int tournamentId);
    }
}