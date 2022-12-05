using System.Linq.Expressions;
using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.Data;
using AscoreStore.Core.Extensions;
using AscoreStore.Core.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AscoreStore.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PaginatedList<Product>> GetAllAsync(Expression<Func<Product, bool>> expression, int? pageNumber, int? pageSize)
        {
            return _context.Products.Where(expression).Include(p => p.Image).AsNoTracking().ToPaginatedList(pageNumber, pageSize);
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.Include(p => p.Image).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int code)
        {
            return await _context.Products.AsNoTracking().Include(p => p.Image).Include(p => p.Category).Where(c => c.Category.Code == code).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }


    }
}