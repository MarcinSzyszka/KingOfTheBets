namespace BetsKing.Server.Data.Entity
{
    public class MatchBet : MatchBase
    {
        public int MatchId { get; set; }

        public Match Match { get; set; }

        public int GamblerId { get; set; }

        public Gambler Gambler { get; set; }
    }
}
