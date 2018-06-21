using System;

namespace BetsKing.Shared.ViewModels.Matches
{
    public class UpdateMatchScoreViewModel
    {
        public int Id { get; set; }

        public string TeamAScoreString { get; set; }

        public string TeamBScoreString { get; set; }

        public int? TeamAScore
        {
            get
            {
                var score = -1;
                var success = int.TryParse(TeamAScoreString, out score);

                return success && score > -1 ? new Nullable<int>(score) : null;
            }
        }

        public int? TeamBScore
        {
            get
            {
                var score = -1;
                var success = int.TryParse(TeamBScoreString, out score);

                return success && score > -1 ? new Nullable<int>(score) : null;
            }
        }
    }
}
