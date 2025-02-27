using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.API.Bootstraping
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.AddServiceDefaults();
            builder.Services.AddOpenApi();
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("X-Version"));
            });
            builder.AddNpgsqlDbContext<CatalogContext>("catalogdb", configureDbContextOptions: options =>
            {
                options.UseNpgsql(builder => { });
            });
        }
    }
}
