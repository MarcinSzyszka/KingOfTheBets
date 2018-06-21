using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Tournaments;

namespace BetsKing.Server.Services.Tournaments
{
    public interface ITournamentService
    {
        Task<Tournament> Create(string name);
        Task<Tournament> Get(int id);
        IEnumerable<Tournament> GetAll();
        Task<IEnumerable<TournamentGamblerViewModel>> GetAllGamblers(int id);
        Task<bool> SetGamblers(SetTournamentGamblersViewModel model);
        Task<IEnumerable<Tournament>> GetForGambler(int gamblerId);
        Task<TournamentResultsViewModel> GetResults(int id);
    }
}