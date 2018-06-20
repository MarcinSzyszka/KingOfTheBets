using BetsKing.Shared.ViewModels.Matches;
using System.Collections.Generic;

namespace BetsKing.Shared.ViewModels.Rounds
{
    public class RoundViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public string TournamentName { get; set; }

        public List<MatchViewModel> Matches { get; set; }
    }
}
