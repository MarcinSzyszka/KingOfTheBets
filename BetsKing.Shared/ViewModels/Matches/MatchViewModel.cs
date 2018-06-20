namespace BetsKing.Shared.ViewModels.Matches
{
    public class MatchViewModel
    {
        public int Id { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int? TeamAScore { get; set; }

        public int? TeamBScore { get; set; }

        public string TeamAScoreString { get; set; }

        public string TeamBScoreString { get; set; }

        public int RoundId { get; set; }

        public string RoundName { get; set; }
    }
}
