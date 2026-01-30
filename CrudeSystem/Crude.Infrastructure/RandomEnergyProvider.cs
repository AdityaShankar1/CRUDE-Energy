using Crude.Core.Interfaces;

namespace Crude.Infrastructure.Data;

public class RandomEnergyProvider
{
    // Logic to generate random double values between a range
    public double GetLatestReading(Guid nodeId) => 
        new Random().NextDouble() * (500 - 10) + 10;
}