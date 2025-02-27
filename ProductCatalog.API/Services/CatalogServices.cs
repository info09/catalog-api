namespace ProductCatalog.API.Services
{
    public class CatalogServices(ILogger<CatalogServices> logger)
    {
        public ILogger<CatalogServices> Logger => logger;
    }
}
