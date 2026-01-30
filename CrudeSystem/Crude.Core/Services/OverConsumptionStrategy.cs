using Crude.Core.Interfaces;

namespace Crude.Core.Services;

public class OverConsumptionStrategy : IEvaluationStrategy
{
    public bool IsViolation(double reading, double threshold) => reading > threshold;

    public string GetFailureMessage(string name, double reading) => 
        $"ALERT: {name} is drawing {reading}kW, exceeding the safety limit!";
}