using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter.Interpreters
{
    public interface IFilterInterpreterFactory
    {
        IFilterTypeInterpreter<TType> Create<TType>(FilterItem filterItem);
    }
}
