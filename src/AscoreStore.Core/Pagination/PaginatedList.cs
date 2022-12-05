using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Core.Pagination
{
    public class PaginatedList<T> : List<T>, IPaginatedList where T : IAggregateRoot
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }


        public PaginatedList(IQueryable<T> source, int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber ?? 0;
            PageSize = pageSize ?? 30;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Skip(PageNumber * PageSize).Take(PageSize));
        }
    }
}