using AutoMapper;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.API.Services
{
    public class CatalogServices(ILogger<CatalogServices> logger, CatalogContext context, IMapper mapper)
    {
        public ILogger<CatalogServices> Logger => logger;
        public CatalogContext Context => context;
        public IMapper Mapper => mapper;
    }
}
