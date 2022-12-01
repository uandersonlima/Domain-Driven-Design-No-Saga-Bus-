using System.Linq.Expressions;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter.Interpreters
{
    public class LessThanInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public LessThanInterpreter(FilterItem filterItem) : base(filterItem)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
            => Expression.LessThan(property, constant);
    }
}
