namespace AscoreStore.Core.Pagination
{
    public interface IPaginatedList
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
    }
}