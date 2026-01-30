using Crude.Core.Interfaces;
using Crude.Core.Services;
using Crude.Infrastructure.Data;
using Crude.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crude.Api;

public class EnergyMonitoringWorker : BackgroundService
{
    private readonly ILogger<EnergyMonitoringWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly EvaluationEngine _engine;
    private readonly RandomEnergyProvider _dataProvider;

    public EnergyMonitoringWorker(
        ILogger<EnergyMonitoringWorker> logger,
        IServiceScopeFactory scopeFactory, // Used to get DB context
        EvaluationEngine engine,
        RandomEnergyProvider dataProvider)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _engine = engine;
        _dataProvider = dataProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var assets = await context.Assets.ToListAsync(stoppingToken);

                // Inside the foreach loop in EnergyMonitoringWorker.cs
                foreach (var asset in assets)
                {
                    asset.CurrentReading = _dataProvider.GetLatestReading(asset.Id);
    
                    // SOLID: Still uses the engine, but now with dynamic data
                    _engine.CheckNode(asset, asset.MaxConsumptionThreshold); 
    
                    asset.LastUpdated = DateTime.UtcNow;
                }

                await context.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Worker: Updated {Count} assets at {Time}", assets.Count, DateTime.Now);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}