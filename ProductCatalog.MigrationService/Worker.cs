using Microsoft.Extensions.Hosting;

namespace ProductCatalog.MigrationService;

public class Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}
