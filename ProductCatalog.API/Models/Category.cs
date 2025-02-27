namespace ProductCatalog.API.Models
{
    public record CategoryCreate(Guid? ParentId, string Name, string Description);
    public record Category(Guid Id, Guid? ParentId, string Name, string Description) : CategoryCreate(ParentId, Name, Description);
}
