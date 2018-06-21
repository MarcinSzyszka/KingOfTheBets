using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;

namespace BetsKing.Server.Services.Matches
{
    public interface IMatchBetService
    {
        Task<bool> AddOrUpdateGamblerMatchBets(int tournamentId, int gamlberId);
        Task<IEnumerable<MatchBet>> SaveBets(SaveMatchBetsViewModel model);
    }
}