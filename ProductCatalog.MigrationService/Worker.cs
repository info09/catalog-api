using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Infrastructure.Data;
using System.Diagnostics;

namespace ProductCatalog.MigrationService;

public class Worker(IHostApplicationLifetime hostApplicationLifetime, IServiceProvider serviceProvider) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = activitySource.StartActivity("Migrate database ...", ActivityKind.Client);
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();

            await EnsureDatabaseAsync(context, stoppingToken);
            await RunMigrationAsync(context, stoppingToken);
            await SeedDataAsync(context, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }
        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(CatalogContext context, CancellationToken cancellationToken)
    {
        var dbCreator = context.GetService<IRelationalDatabaseCreator>();
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(CatalogContext context, CancellationToken cancellationToken)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await context.Database.MigrateAsync();
        });
    }

    private static Task SeedDataAsync(CatalogContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
