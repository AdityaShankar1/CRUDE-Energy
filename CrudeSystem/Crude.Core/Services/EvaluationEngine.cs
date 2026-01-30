using Crude.Core.Interfaces;

namespace Crude.Core.Services;

public class EvaluationEngine
{
    private readonly IEvaluationStrategy _strategy;

    // Dependency Injection: We pass the strategy in (DIP)
    public EvaluationEngine(IEvaluationStrategy strategy)
    {
        _strategy = strategy;
    }

    public void CheckNode(IEnergyNode node, double threshold)
    {
        if (_strategy.IsViolation(node.CurrentReading, threshold))
        {
            node.Status = NodeStatus.Flagged;
            // In a real app, this is where we would trigger the Observer/Event
            Console.WriteLine(_strategy.GetFailureMessage(node.Name, node.CurrentReading));
        }
        else if (node.Status == NodeStatus.Flagged)
        {
            node.Status = NodeStatus.Healthy; // Auto-resolve if back to normal
        }
    }
}