namespace AscoreStore.Core.Filter.Models
{
    public class PaginationFilter
    {
        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();
        public int Take { get; set; }
        public int Skip { get; set; }

    }
}