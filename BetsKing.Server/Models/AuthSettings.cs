namespace BetsKing.Server.Models
{
    public class AuthSettings
    {
        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public double TokenLifeTimeInMinutes { get; set; }
    }
}
