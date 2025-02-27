
namespace ProductCatalog.API.Apis
{
    public static class CatalogApi
    {
        public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
        {
            var vApi = app.NewVersionedApi("Catalog");
            var v1 = vApi.MapGroup("/api/v{version:apiVersion}/catalog").HasApiVersion(1, 0);

            v1.MapGet("/categories", GetAllCategories).WithName("ListCategories").WithSummary("List Categories");
            v1.MapPost("/categories", CreateCategory).WithName("CreateCategory").WithSummary("Create Category");

            v1.MapGet("/brands", GetAllBrands).WithName("ListBrands").WithSummary("List Brands");
            v1.MapPost("/brands", CreateBrand).WithName("CreateBrand").WithSummary("Create Brand");

            v1.MapGet("/products", GetAllProducts).WithName("ListProducts").WithSummary("List Productcs");
            v1.MapGet("/products/{id}", GetProductById).WithName("GetProductById").WithSummary("Get product by id");
            v1.MapPost("/products", CreateProduct).WithName("CreateProduct").WithSummary("Create Product");

            v1.MapGet("/products/{id:guid}/dimensions", GetProductDimensions).WithName("GetProductDimensions").WithSummary("Get product dimensions");
            v1.MapPost("/products/{id:guid}/dimensions", CreateDimensions).WithName("CreateDimensions").WithSummary("Create dimensions");
            v1.MapPost("/products/{id:guid}/dimensions/{id:guid}/values", CreateDimensionValues).WithName("CreateDimensionValues").WithSummary("Create dimension values");

            return app;
        }

        private static async Task CreateDimensionValues(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task CreateDimensions(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetProductDimensions(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetProductById(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task CreateProduct(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetAllProducts(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task CreateBrand(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetAllBrands(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task CreateCategory(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static async Task GetAllCategories(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
