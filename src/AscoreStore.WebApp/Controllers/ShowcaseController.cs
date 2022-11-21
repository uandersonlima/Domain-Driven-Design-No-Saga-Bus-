using AscoreStore.Catalog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AscoreStore.WebApp.Controllers
{
    public class ShowcaseController : Controller
    {
        private readonly IProductAppService _produtoAppService;

        public ShowcaseController(IProductAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index()
        {
            return View(await _produtoAppService.GetAllAsync());
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View(await _produtoAppService.GetByIdAsync(id));
        }
    }
}