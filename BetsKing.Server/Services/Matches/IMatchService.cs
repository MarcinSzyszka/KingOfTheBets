using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;

namespace BetsKing.Server.Services.Matches
{
    public interface IMatchService
    {
        Task<Match> Create(AddMatchViewModel model);
        Task<Match> Get(int id);
        Task<IEnumerable<Match>> GetAll(int roundId);
        Task<bool> UpdateScore(UpdateMatchScoreViewModel model);
    }
}