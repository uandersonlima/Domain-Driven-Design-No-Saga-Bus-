using AscoreStore.Core.Filter.Models;
using System.Linq.Expressions;

namespace AscoreStore.Core.Filter.Interpreters
{
    public class ContainsInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public ContainsInterpreter(FilterItem filterItem) : base(filterItem)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        {
            var method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

            return Expression.Call(property, method, constant);
        }
    }
}
