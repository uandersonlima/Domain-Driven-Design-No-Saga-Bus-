using AscoreStore.Catalog.Application.Services.Interfaces;
using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.Filter;
using AscoreStore.Core.Filter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AscoreStore.WebApp.Controllers
{
    public class ShowcaseController : Controller
    {
        private readonly IDynamicFilter _dynamicFilter;
        private readonly IProductAppService _productAppService;


        public ShowcaseController(IDynamicFilter dynamicFilter, IProductAppService productAppService)
        {
            _dynamicFilter = dynamicFilter;
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index(PaginationFilter paginationFilter)
        {
            var expressionDynamic = paginationFilter.Filters.Count > 0 ?
                                    _dynamicFilter.FromFilterItemList<Product>(paginationFilter.Filters)
                                    : t => true;
            return View(await _productAppService.GetAllAsync(expressionDynamic));
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View(await _productAppService.GetByIdAsync(id));
        }
    }
}