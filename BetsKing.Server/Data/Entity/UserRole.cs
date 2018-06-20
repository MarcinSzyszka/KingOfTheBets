namespace BetsKing.Server.Data.Entity
{
    public class UserRole
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int GamblerId { get; set; }

        public Gambler Gambler { get; set; }
    }
}
