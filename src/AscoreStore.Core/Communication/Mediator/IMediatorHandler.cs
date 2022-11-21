using AscoreStore.Core.Messages;

namespace AscoreStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T ev) where T : Event;
    }
}