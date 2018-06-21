using System.Collections.Generic;

namespace BetsKing.Shared.ViewModels.Tournaments
{
    public class TournamentResultsViewModel
    {
        public int TournamentId { get; set; }

        public List<TournamentGamblerResultsViewModel> GamblersResults { get; set; }
    }
}
