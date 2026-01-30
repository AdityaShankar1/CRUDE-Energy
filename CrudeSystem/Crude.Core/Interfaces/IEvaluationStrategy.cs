
namespace Crude.Core.Interfaces;

public interface IEvaluationStrategy
{
    // Returns true if there is a violation
    bool IsViolation(double currentReading, double threshold);
    string GetFailureMessage(string nodeName, double reading);
}