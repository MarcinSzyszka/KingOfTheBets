namespace BetsKing.Shared.ViewModels.Tournaments
{
    public class TournamentGamblerResultsViewModel
    {
        public int GamblerId { get; set; }

        public int TorunamentId { get; set; }

        public string GamblerDisplayName { get; set; }

        public int PointsForTypingMatchResult { get; set; }

        public int PointsForTypingExactMatchScore { get; set; }

        public int GeneralPoints => PointsForTypingExactMatchScore + PointsForTypingMatchResult;
    }
}
