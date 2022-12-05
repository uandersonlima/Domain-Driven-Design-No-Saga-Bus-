namespace AscoreStore.Core.Filter.Models
{
    public class PaginationFilter
    {
        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();
        public int Skip { get; set; }
        public int Take { get; set; }

    }
}