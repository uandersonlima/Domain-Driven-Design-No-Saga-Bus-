using System.Linq.Expressions;
using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.Data;

namespace AscoreStore.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression);
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByCategoryAsync(int code);
        Task<IEnumerable<Category>> GetCategoriesAsync();

        void Add(Product product);
        void Update(Product product);

        void Add(Category category);
        void Update(Category category);
    }
}