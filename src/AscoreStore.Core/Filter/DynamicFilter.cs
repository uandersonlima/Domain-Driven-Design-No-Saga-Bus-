using System.Linq.Expressions;
using AscoreStore.Core.Extensions;
using AscoreStore.Core.Filter.Interpreters;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter
{
    public class DynamicFilter : IDynamicFilter
    {
        private readonly IFilterInterpreterFactory _factory;

        public DynamicFilter(IFilterInterpreterFactory factory)
        {
            _factory = factory;
        }

        public Expression<Func<TType, bool>> FromFilterItemList<TType>(IReadOnlyList<FilterItem> filterItems)
        {
            var expression = filterItems.Count > 0 ?
             filterItems
                .Select(filterItem =>
                {
                    var interpreter = _factory.Create<TType>(filterItem);
                    return ResolveNextInterpreter(interpreter, filterItem);
                })
                .Aggregate((curr, next) => curr.And(next))
                .Interpret()
            : t => true;

            return expression;
        }

        private IFilterTypeInterpreter<TType> ResolveNextInterpreter<TType>(IFilterTypeInterpreter<TType> interpreter, FilterItem filterItem)
        {
            if (filterItem.Or != null)
                return interpreter.Or(_factory.Create<TType>(filterItem.Or));

            if (filterItem.And != null)
                return interpreter.And(_factory.Create<TType>(filterItem.And));

            return interpreter;
        }

    }
}