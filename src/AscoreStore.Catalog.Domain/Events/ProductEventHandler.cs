using AscoreStore.Catalog.Domain.Interfaces;
using MediatR;

namespace AscoreStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBelowStockEvent notification, CancellationToken cancellationToken)
        {
            var produto = await _productRepository.GetByIdAsync(notification.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }
    }
}