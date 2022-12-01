namespace AscoreStore.Core.Filter.Models
{
    public class FilterItem
    {
        public string Property { get; set; }
        public string FilterType { get; set; }
        //public object Value { get; set; }
        public string Value { get; set; }
        public FilterItem? Or { get; set; }
        public FilterItem? And { get; set; }
    }
}
