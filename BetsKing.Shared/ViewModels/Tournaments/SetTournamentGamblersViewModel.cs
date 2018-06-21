using System.Collections.Generic;

namespace BetsKing.Shared.ViewModels.Tournaments
{
    public class SetTournamentGamblersViewModel
    {
        public int TournamentId { get; set; }

        public IEnumerable<int> GamblersIds { get; set; }
    }
}
