namespace BetsKing.Shared.ViewModels.Tournaments
{
    public class TournamentGamblerViewModel
    {
        public int GamblerId { get; set; }

        public int TournamentId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public bool Participates { get; set; }
    }
}
