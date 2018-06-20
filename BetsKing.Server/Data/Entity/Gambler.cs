using System.Collections.Generic;

namespace BetsKing.Server.Data.Entity
{
    public class Gambler : BaseEntity
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public ICollection<MatchBet> Bets { get; set; }

        public ICollection<TournamentGambler> Tournaments { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}
