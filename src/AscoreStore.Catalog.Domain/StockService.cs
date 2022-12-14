using AscoreStore.Catalog.Domain.Events;
using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.DomainObjects.DTO;
using AscoreStore.Core.Messages.Common.Notifications;

namespace AscoreStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediator;

        public StockService(IProductRepository productRepository, IMediatorHandler mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }

        public async Task<bool> DecreaseStockAsync(Guid productId, int quantity)
        {
            if (!await DecreaseItemStock(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DecreaseOrderProductList(OrderProductList orderProductList)
        {
            foreach (var item in orderProductList.Items)
            {
                if (!await DecreaseItemStock(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> IncreaseStockAsync(Guid productId, int quantity)
        {
            var succeeded = await IncreaseItemStock(productId, quantity);

            if (!succeeded) return false;

            return await _productRepository.UnitOfWork.Commit();
        }


        public async Task<bool> IncreaseOrderProductList(OrderProductList orderProductList)
        {
            foreach (var item in orderProductList.Items)
            {
                await IncreaseItemStock(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.Commit();
        }
        private async Task<bool> DecreaseItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null) return false;

            if (!product.HaveStock(quantity))
            {
                await _mediator.PublishNotificationAsync(new DomainNotification("Estoque", $"Produto - {product.Name} sem estoque"));
                return false;
            }

            product.DecreaseStock(quantity);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (product.StockQuantity < 10)
            {
                await _mediator.PublishEventAsync(new ProductBelowStockEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return true;
        }
        private async Task<bool> IncreaseItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null) return false;

            product.IncreaseStock(quantity);
            _productRepository.Update(product);

            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }


    }

}