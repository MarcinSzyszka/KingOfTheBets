using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Rounds;

namespace BetsKing.Server.Services.Rounds
{
    public interface IRoundService
    {
        Task<Round> Create(string name, int tournamentId);
        Task<Round> Get(int id);
        Task<IEnumerable<Round>> GetAll(int tournamentId);
        Task<IEnumerable<RoundWithBetsViewModel>> GetByTournamentAndGambler(int tournamentId, int gamblerId);
    }
}