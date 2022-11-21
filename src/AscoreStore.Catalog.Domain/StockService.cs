using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Core.Communication.Mediator;

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
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                return false;

            if (!product.HaveStock(quantity))
                return false;

            product.DecreaseStock(quantity);

            // TODO: Parametrizar a quantidade de estoque baixo
            if (product.StockQuantity < 10)
            {
                //await _mediator.PublishEventAsync(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> IncreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                return false;

            product.IncreaseStock(quantity);

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}