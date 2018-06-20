﻿using System;

namespace BetsKing.Shared.ViewModels.Matches
{
    public class AddMatchViewModel
    {
        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public string TeamAScoreString { get; set; }

        public string TeamBScoreString { get; set; }

        public int RoundId { get; set; }

        public int? TeamAScore
        {
            get
            {
                var score = -1;
                int.TryParse(TeamAScoreString, out score);

                return score > -1 ? new Nullable<int>(score) : null;
            }
        }

        public int? TeamBScore
        {
            get
            {
                var score = -1;
                int.TryParse(TeamBScoreString, out score);

                return score > -1 ? new Nullable<int>(score) : null;
            }
        }
    }
}
