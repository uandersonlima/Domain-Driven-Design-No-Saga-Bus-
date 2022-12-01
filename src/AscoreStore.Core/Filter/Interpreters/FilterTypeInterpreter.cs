using AscoreStore.Core.Filter.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace AscoreStore.Core.Filter.Interpreters
{
    public abstract class FilterTypeInterpreter<TType> : IFilterTypeInterpreter<TType>
    {
        private FilterItem _filterItem;

        public FilterTypeInterpreter(FilterItem filterItem)
        {
            _filterItem = filterItem;
        }

        public Expression<Func<TType, bool>> Interpret()
        {
            var dynamicType = typeof(TType);
            var parameter = Expression.Parameter(dynamicType, dynamicType.Name.First().ToString());
            var property = Expression.Property(parameter, _filterItem.Property);
            var propertyInfo = (PropertyInfo)property.Member;
            var value = Convert.ChangeType(_filterItem.Value.ToString(), propertyInfo.PropertyType);
            var constant = Expression.Constant(value);
            var expression = CreateExpression(property, constant);

            return Expression.Lambda<Func<TType, bool>>(expression, parameter);
        }

        internal abstract Expression CreateExpression(MemberExpression property, ConstantExpression constant);
    }
}
