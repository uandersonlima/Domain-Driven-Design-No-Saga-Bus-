using AscoreStore.Core.Filter.Interpreters;

namespace AscoreStore.Core.Filter.Extensions
{
    public static class InterpreterTypeExtensions
    {
        public static IFilterTypeInterpreter<TType> And<TType>(this IFilterTypeInterpreter<TType> left, IFilterTypeInterpreter<TType> right)
            => new AndInterpreter<TType>(left, right);

        public static IFilterTypeInterpreter<TType> Or<TType>(this IFilterTypeInterpreter<TType> left, IFilterTypeInterpreter<TType> right)
            => new OrInterpreter<TType>(left, right);
    }
}