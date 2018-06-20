using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;

namespace BetsKing.Server.Services.Tournaments
{
    public interface ITournamentService
    {
        Task<Tournament> Create(string name);
        Task<Tournament> Get(int id);
        IEnumerable<Tournament> GetAll();
    }
}