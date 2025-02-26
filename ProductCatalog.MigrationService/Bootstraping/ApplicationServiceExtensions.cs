using Microsoft.EntityFrameworkCore;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.Api.MigrationService.Bootstraping
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.AddNpgsqlDbContext<CatalogContext>("catalogdb", configureDbContextOptions: dbContextOptions =>
            {
                dbContextOptions.UseNpgsql(builder =>
                {
                    builder.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);
                });
            });
        }
    }
}
