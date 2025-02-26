using ProductCatalog.Api.MigrationService.Bootstraping;
using ProductCatalog.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.AddApplicationServices();

var host = builder.Build();
host.Run();
