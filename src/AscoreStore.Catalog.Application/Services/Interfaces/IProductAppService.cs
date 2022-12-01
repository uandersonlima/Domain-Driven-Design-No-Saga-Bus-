using System.Linq.Expressions;
using AscoreStore.Catalog.Application.ViewModels;
using AscoreStore.Catalog.Domain.ProductAggregate;

namespace AscoreStore.Catalog.Application.Services.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategoryAsync(int code);
        Task<ProductViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression);
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        Task AddProductAsync(ProductViewModel productViewModel);
        Task UpdateProductAsync(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseStockAsync(Guid id, int quantity);
        Task<ProductViewModel> IncreaseStockAsync(Guid id, int quantity);
    }
}