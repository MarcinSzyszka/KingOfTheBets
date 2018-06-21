namespace BetsKing.Shared.ViewModels.Matches
{
    public class MatchBetViewModel
    {
        public int Id { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int? TeamAScoreBet { get; set; }

        public int? TeamBScoreBet { get; set; }

        public string TeamAScoreString { get; set; }

        public string TeamBScoreString { get; set; }

        public int RoundId { get; set; }

        public string RoundName { get; set; }
    }
}
