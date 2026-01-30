using Crude.Core.Interfaces;

namespace Crude.Core.Entities;

public class EnergyAsset : IEnergyNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public double CurrentReading { get; set; }
    public NodeStatus Status { get; set; } = NodeStatus.Healthy;
    
    // Resume Polish: Add a timestamp for when the reading last changed
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public double MaxConsumptionThreshold { get; set; } = 400.0; // Default
}