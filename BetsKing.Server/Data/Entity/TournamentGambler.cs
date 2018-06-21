namespace BetsKing.Server.Data.Entity
{
    public class TournamentGambler
    {
        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public int GamblerId { get; set; }

        public Gambler Gambler { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
