namespace Crude.Core.Interfaces;

public enum NodeStatus { Healthy, Warning, Flagged, Maintenance }

public interface IEnergyNode
{
    Guid Id { get; }
    string Name { get; }
    double CurrentReading { get; } // Positive for Source, Negative for Sink
    NodeStatus Status { get; set; }
}