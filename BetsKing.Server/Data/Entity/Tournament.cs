using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Tournament : BaseEntity
    {
        public string Name { get; set; }

        public bool IsArchive { get; set; }

        public ICollection<Round> Rounds { get; set; }

        public ICollection<TournamentGambler> Gamblers { get; set; }
    }
}
