using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Match : BaseEntity
    {
        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int? TeamAScore { get; set; } = null;

        public int? TeamBScore { get; set; } = null;

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public ICollection<MatchBet> Bets { get; set; }
    }
}
