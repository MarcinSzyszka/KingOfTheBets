using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Match : BaseEntity
    {
        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int? TeamAScore { get; set; }

        public int? TeamBScore { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public ICollection<MatchBet> Bets { get; set; }
    }
}
