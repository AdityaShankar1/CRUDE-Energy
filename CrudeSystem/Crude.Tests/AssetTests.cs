using Xunit;
using Crude.Core.Services;

namespace Crude.Tests;

public class AssetTests
{
    [Fact]
    public void IsViolation_ShouldReturnTrue_WhenReadingExceedsThreshold()
    {
        // Arrange
        var strategy = new OverConsumptionStrategy();
        double reading = 1000.5;
        double threshold = 800.0;

        // Act
        // This matches your specific method: IsViolation(double, double)
        bool result = strategy.IsViolation(reading, threshold);

        // Assert
        Assert.True(result, "The strategy should identify 1000.5kW as a violation of the 800kW limit.");
    }

    [Fact]
    public void GetFailureMessage_ShouldFormatCorrectly()
    {
        // Arrange
        var strategy = new OverConsumptionStrategy();
        string assetName = "Main Transformer";
        double reading = 950.0;

        // Act
        string message = strategy.GetFailureMessage(assetName, reading);

        // Assert
        Assert.Contains("Main Transformer", message);
        Assert.Contains("950", message);
        Assert.StartsWith("ALERT:", message);
    }
}