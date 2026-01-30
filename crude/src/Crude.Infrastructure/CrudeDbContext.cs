using Microsoft.EntityFrameworkCore;
using Crude.Domain;

namespace Crude.Infrastructure
{
    public class CrudeDbContext : DbContext
    {
        public CrudeDbContext(DbContextOptions<CrudeDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<MaintenanceTicket> Tickets => Set<MaintenanceTicket>();
        public DbSet<TelemetryRecord> Telemetry => Set<TelemetryRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Discriminator for Asset types (Pipeline, Producer, Consumer, Storage)
            modelBuilder.Entity<Asset>().HasDiscriminator(a => a.Type);

            // Unique username constraint
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}