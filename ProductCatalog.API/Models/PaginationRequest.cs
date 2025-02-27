namespace ProductCatalog.API.Models
{
    public class PaginationRequest(int pageIndex = 0, int pageSize = 20)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
    }
}
