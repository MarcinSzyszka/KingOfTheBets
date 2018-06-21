namespace BetsKing.Server.Data.Entity
{
    public class MatchBet : BaseEntity
    {
        public int MatchId { get; set; }

        public Match Match { get; set; }

        public int GamblerId { get; set; }

        public Gambler Gambler { get; set; }

        public int? TeamAScoreBet { get; set; } = null;

        public int? TeamBScoreBet  { get; set; } = null;
    }
}
