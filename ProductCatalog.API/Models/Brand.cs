namespace ProductCatalog.API.Models
{
    public record BrandCreate(string Name, string Description);
    public record Brand(Guid Id, string Name, string Description):BrandCreate(Name, Description);
}
