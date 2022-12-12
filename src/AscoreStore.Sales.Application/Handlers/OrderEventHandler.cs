using AscoreStore.Sales.Application.Events;
using MediatR;

namespace AscoreStore.Sales.Application.Handlers
{
    public class OrderEventHandler : INotificationHandler<DraftOrderStartedEvent>,
            INotificationHandler<UpdatedOrderEvent>,
        INotificationHandler<OrderItemAddedEvent>

    {
        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(DraftOrderStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}