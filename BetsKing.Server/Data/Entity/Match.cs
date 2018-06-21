using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Match : MatchBase
    {
        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public ICollection<MatchBet> Bets { get; set; }
    }
}
