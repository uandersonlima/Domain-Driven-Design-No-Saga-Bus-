using System.Linq.Expressions;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter.Interpreters
{
    public class EqualsInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public EqualsInterpreter(FilterItem filterItem) : base(filterItem)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant) 
            => Expression.Equal(property, constant);
    }
}
