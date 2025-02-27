namespace ProductCatalog.API.Models
{
    public class PagedResult<T>(int index, int pageSize, long count, IEnumerable<T> items) where T : class
    {
        public int Index => index;
        public int PageSize => pageSize;
        public long Count => count;
        public IEnumerable<T> Items => items;
    }
}
