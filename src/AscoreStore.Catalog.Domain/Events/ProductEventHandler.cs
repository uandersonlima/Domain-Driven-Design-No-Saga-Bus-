using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using MediatR;

namespace AscoreStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent>,
    INotificationHandler<OrderStartedEvent>,
    INotificationHandler<OrderProcessingCanceledEvent>

    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBelowStockEvent notification, CancellationToken cancellationToken)
        {
            var produto = await _productRepository.GetByIdAsync(notification.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }

        public async Task Handle(OrderStartedEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DecreaseOrderProductList(message.OrderProductList);

            if (result)
            {
                await _mediatorHandler.PublishEventAsync(new ConfirmedStockOrderEvent(message.OrderId, message.CustomerId, message.OrderProductList, message.Total, message.CardName, message.CardNumber, message.CardExpiration, message.CardCvv));
            }
            else
            {
                await _mediatorHandler.PublishEventAsync(new RejectedStockOrderEvent(message.OrderId, message.CustomerId));
            }
        }

        public async Task Handle(OrderProcessingCanceledEvent message, CancellationToken cancellationToken)
        {
            await _stockService.IncreaseOrderProductList(message.OrderProductList);
        }
    }
}