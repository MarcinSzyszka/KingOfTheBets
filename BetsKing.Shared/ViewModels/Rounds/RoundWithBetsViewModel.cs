using BetsKing.Shared.ViewModels.Matches;
using System.Collections.Generic;

namespace BetsKing.Shared.ViewModels.Rounds
{
    public class RoundWithBetsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public string TournamentName { get; set; }

        public IEnumerable<MatchBetViewModel> MatchBets { get; set; }

        /// <summary>
        /// Property used in Client
        /// </summary>
        public bool Collapsed { get; set; } = true;
    }
}
