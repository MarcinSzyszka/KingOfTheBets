using BetsKing.Server.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BetsKing.Server.Data.Context
{
    public class BetsKingDbContext : DbContext
    {
        public BetsKingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Gambler> Gamblers { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchBet> MatchBets { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentGambler> TournamentGamblers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
            .Property(b => b.TeamAScore)
            .IsRequired(false);

             modelBuilder.Entity<Match>()
            .Property(b => b.TeamBScore)
            .IsRequired(false);

            modelBuilder.Entity<MatchBet>()
            .Property(b => b.TeamAScoreBet)
            .IsRequired(false);

             modelBuilder.Entity<MatchBet>()
            .Property(b => b.TeamBScoreBet)
            .IsRequired(false);

            modelBuilder.Entity<TournamentGambler>()
                .HasKey(t => new { t.TournamentId, t.GamblerId });
            modelBuilder.Entity<TournamentGambler>()
                .HasOne(pt => pt.Gambler)
                .WithMany(p => p.Tournaments)
                .HasForeignKey(pt => pt.GamblerId);
            modelBuilder.Entity<TournamentGambler>()
                .HasOne(pt => pt.Tournament)
                .WithMany(t => t.Gamblers)
                .HasForeignKey(pt => pt.TournamentId);

            modelBuilder.Entity<UserRole>()
                .HasKey(t => new { t.RoleId, t.GamblerId });
            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Gambler)
                .WithMany(p => p.Roles)
                .HasForeignKey(pt => pt.GamblerId);
            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.Gamblers)
                .HasForeignKey(pt => pt.RoleId);
        }
    }
}
