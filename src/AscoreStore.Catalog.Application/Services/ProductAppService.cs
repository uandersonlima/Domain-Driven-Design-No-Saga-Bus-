using System.Linq.Expressions;
using AscoreStore.Catalog.Application.Services.Interfaces;
using AscoreStore.Catalog.Application.ViewModels;
using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.DomainObjects;
using AutoMapper;

namespace AscoreStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, IStockService stockService, IMapper mapper)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategoryAsync(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategoryAsync(code));
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAllAsync(expression));
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategoriesAsync());
        }

        public async Task AddProductAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProductAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DecreaseStockAsync(Guid id, int quantity)
        {
            if (!await _stockService.DecreaseStockAsync(id, quantity))
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<ProductViewModel> IncreaseStockAsync(Guid id, int quantity)
        {
            if (!_stockService.IncreaseStockAsync(id, quantity).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }

    }
}