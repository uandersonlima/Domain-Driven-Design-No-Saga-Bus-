using System.Linq.Expressions;

namespace AscoreStore.Core.Filter.Interpreters
{
    public interface IFilterTypeInterpreter<TType>
    {
        Expression<Func<TType, bool>> Interpret();
    }
}