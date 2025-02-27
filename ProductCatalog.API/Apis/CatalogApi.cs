
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models;
using ProductCatalog.API.Services;
using System.ComponentModel;

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
            v1.MapPost("/products/{id:guid}/dimensions/values", CreateDimensionValues)
                    .WithName("CreateDimensionValues")
                    .WithSummary("Create values for a dimension");

            return app;
        }

        private static async Task<Results<Created, NotFound>> CreateDimensionValues([AsParameters] CatalogServices services, [Description("Dimension id")] Guid id, DimensionCreate dimension)
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

        private static async Task<Created> CreateBrand([AsParameters] CatalogServices catalogServices, BrandCreate brandCreate)
        {
            var entity = catalogServices.Mapper.Map<Infrastructure.Entities.Brand>(brandCreate);
            entity.Id = Guid.CreateVersion7();

            catalogServices.Context.Brands.Add(entity);
            await catalogServices.Context.SaveChangesAsync();

            return TypedResults.Created($"/api/catalog/v1/brands/{entity.Id}");
        }

        private static async Task<Ok<PagedResult<Brand>>> GetAllBrands([AsParameters] PaginationRequest request, [AsParameters] CatalogServices catalogServices)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;

            var query = catalogServices.Context.Brands;
            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name).Skip(pageIndex * pageSize).Take(pageSize).Select(i => catalogServices.Mapper.Map<Brand>(i)).ToListAsync();

            return TypedResults.Ok(new PagedResult<Brand>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        private static async Task<Created> CreateCategory([AsParameters]CatalogServices catalogServices, CategoryCreate category)
        {
            var entity = catalogServices.Mapper.Map<Infrastructure.Entities.Category>(category);
            entity.Id = Guid.CreateVersion7();

            catalogServices.Context.Categories.Add(entity);
            await catalogServices.Context.SaveChangesAsync();

            return TypedResults.Created($"/api/catalog/v1/brands/{entity.Id}");
        }

        private static async Task<Ok<PagedResult<Category>>> GetAllCategories(
            [AsParameters] PaginationRequest request,
            [AsParameters] CatalogServices catalogServices,
            [Description("Id of parent, use Guild.Empty to get root Categories")] Guid? parentId)
        {
            var pageIndex = request.PageIndex;
            var pageSize = request.PageSize;

            if (parentId == Guid.Empty)
                parentId = null;

            var query = catalogServices.Context.Categories.Where(i => i.ParentId == parentId);
            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name).Skip(pageIndex * pageSize).Take(pageSize).Select(i => catalogServices.Mapper.Map<Category>(i)).ToListAsync();

            return TypedResults.Ok(new PagedResult<Category>(pageIndex, pageSize, totalItems, itemsOnPage));
        }
    }
}
