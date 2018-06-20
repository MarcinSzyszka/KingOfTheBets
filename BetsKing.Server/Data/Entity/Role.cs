using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> Gamblers { get; set; }
    }
}
