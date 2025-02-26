var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("latest")
    .WithVolume("catalog_data", "/var/lib/postgresql/data")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin((rbuilder) => {
        rbuilder
            .WithImageTag("latest");
    });

var catalogDb = postgres.AddDatabase("catalogdb", "catalog");

var migrationService = builder.AddProject<Projects.ProductCatalog_MigrationService>("productcatalog-migrationservice");

builder.AddProject<Projects.ProductCatalog_API>("productcatalog-api").WithReference(catalogDb).WaitFor(postgres).WaitForCompletion(migrationService);



builder.Build().Run();
