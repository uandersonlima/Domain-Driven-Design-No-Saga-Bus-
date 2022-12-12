using AscoreStore.Core.Messages;
using AscoreStore.Core.Messages.Common.Notifications;

namespace AscoreStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEventAsync<T>(T ev) where T : Event;
        Task PublishNotificationAsync<T>(T notification) where T : DomainNotification;
        Task<bool> SendCommandAsync<T>(T command) where T : Command;
    }
}