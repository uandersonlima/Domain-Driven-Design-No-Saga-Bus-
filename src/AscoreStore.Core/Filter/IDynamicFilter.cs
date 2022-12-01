using System.Linq.Expressions;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter
{
    public interface IDynamicFilter
    {
        Expression<Func<TType, bool>> FromFilterItemList<TType>(IReadOnlyList<FilterItem> filterItem);
    }
}