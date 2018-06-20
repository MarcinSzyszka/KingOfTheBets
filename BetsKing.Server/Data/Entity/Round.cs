using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Round : BaseEntity
    {
        public string Name { get; set; }

        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
