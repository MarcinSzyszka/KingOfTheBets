using System.Collections.Generic;

namespace BetsKing.Shared.ViewModels.Matches
{
    public class SaveMatchBetsViewModel
    {
        public int GamblerId { get; set; }

        public int RoundId { get; set; }

        public IEnumerable<UpdateMatchBetViewModel> Bets { get; set; }
    }
}
