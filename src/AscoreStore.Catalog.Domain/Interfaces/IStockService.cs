using AscoreStore.Core.DomainObjects.DTO;

namespace AscoreStore.Catalog.Domain.Interfaces
{
    public interface IStockService : IDisposable
    {
        Task<bool> DecreaseStockAsync(Guid productId, int quantity);
        Task<bool> DecreaseOrderProductList(OrderProductList orderProductList);
        Task<bool> IncreaseStockAsync(Guid productId, int quantity);
        Task<bool> IncreaseOrderProductList(OrderProductList orderProductList);
    }
}