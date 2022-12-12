namespace AscoreStore.Core.Filter.Models
{
    public class PaginationFilter
    {
        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}