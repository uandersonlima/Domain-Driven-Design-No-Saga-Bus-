using System.Linq.Expressions;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter.Interpreters
{
    public class GreaterThanInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public GreaterThanInterpreter(FilterItem filterItem) : base(filterItem)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant) 
            => Expression.GreaterThan(property, constant);
    }
}
