using AscoreStore.Core.DomainObjects;
using AscoreStore.Core.Pagination;

namespace AscoreStore.Core.Extensions
{
    public static class PaginatedListExtension
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int? pageNumber, int? pageSize) where T : IAggregateRoot
        {
            return new PaginatedList<T>(source, pageNumber, pageSize);
        }
    }
}