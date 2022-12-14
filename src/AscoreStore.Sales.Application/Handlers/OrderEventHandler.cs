using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using AscoreStore.Sales.Application.Commands;
using AscoreStore.Sales.Application.Events;
using MediatR;

namespace AscoreStore.Sales.Application.Handlers
{
    public class OrderEventHandler : INotificationHandler<DraftOrderStartedEvent>,
        INotificationHandler<UpdatedOrderEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<RejectedStockOrderEvent>,
        INotificationHandler<PaymentMadeEvent>,
        INotificationHandler<PaymentDeclinedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

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

        public async Task Handle(PaymentDeclinedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommandAsync(new CancelOrderProcessingCommand(message.OrderId, message.CustomerId));
        }

        public async Task Handle(PaymentMadeEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommandAsync(new FinalizeOrderCommand(message.OrderId, message.CustomerId));
        }

        public async Task Handle(RejectedStockOrderEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommandAsync(new CancelOrderProcessingReverseStockCommand(message.OrderId, message.CustomerId));
        }
    }
}