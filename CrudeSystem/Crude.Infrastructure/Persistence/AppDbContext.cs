using Microsoft.EntityFrameworkCore;
using Crude.Core.Entities;
using Crude.Core.Interfaces;

namespace Crude.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EnergyAsset> Assets => Set<EnergyAsset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EnergyAsset>()
            .Property(e => e.Status)
            .HasConversion<string>();

        modelBuilder.Entity<EnergyAsset>().HasData(
            new EnergyAsset
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Main Transformer",
                MaxConsumptionThreshold = 1000.0,
                Status = NodeStatus.Healthy
            },
            new EnergyAsset
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Cooling Pump 01",
                MaxConsumptionThreshold = 150.0,
                Status = NodeStatus.Healthy
            },
            new EnergyAsset
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Heavy Drill Rig",
                MaxConsumptionThreshold = 600.0,
                Status = NodeStatus.Healthy
            }
        );
    }
}