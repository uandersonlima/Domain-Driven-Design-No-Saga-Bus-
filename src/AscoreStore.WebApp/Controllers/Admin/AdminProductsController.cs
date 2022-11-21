using AscoreStore.Catalog.Application.Services.Interfaces;
using AscoreStore.Catalog.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AscoreStore.WebApp.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAllAsync());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await FillCategories(new ProductViewModel()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel, IFormFile image)
        {
            if (image is null || !image.ContentType.Equals("image/jpeg"))
            {
                TempData["image"] = "Imagem inválida, por favor forneça uma imagem válida para prosseguir";
                return View(await FillCategories(productViewModel));
            }

            using (var fileStream = image.OpenReadStream())
            {
                var contentType = image.ContentType;
                var name = image.FileName;
                var size = image.Length;
                var data = new byte[size];
                fileStream.Read(data, 0, (int)size);
                productViewModel.Image = new ImageViewModel
                {
                    Name = name,
                    ContentType = contentType,
                    Size = (int)size,
                    Data = data
                };
            }

            ModelState.Remove("Image");
            ModelState.Remove("Categories");

            if (!ModelState.IsValid)
                return View(await FillCategories(productViewModel));


            await _productAppService.AddProductAsync(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await FillCategories(await _productAppService.GetByIdAsync(id)));
        }

        [HttpPost]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel, IFormFile image)
        {
            if (image is null || !image.ContentType.Equals("image/jpeg"))
            {
                TempData["image"] = "Imagem inválida, por favor forneça uma imagem válida para prosseguir";
                return View(await FillCategories(productViewModel));
            }

            var product = await _productAppService.GetByIdAsync(id);
            productViewModel.StockQuantity = product.StockQuantity;

            using (var fileStream = image.OpenReadStream())
            {
                var contentType = image.ContentType;
                var name = image.FileName;
                var size = image.Length;
                var data = new byte[size];
                fileStream.Read(data, 0, (int)size);
                productViewModel.Image = new ImageViewModel
                {
                    Name = name,
                    ContentType = contentType,
                    Size = (int)size,
                    Data = data
                };
            }

            ModelState.Remove("Image");
            ModelState.Remove("Categories");
            ModelState.Remove("StockQuantity");

            if (!ModelState.IsValid) return View(await FillCategories(productViewModel));

            await _productAppService.UpdateProductAsync(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetByIdAsync(id));
        }

        [HttpPost]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantidade)
        {
            if (quantidade > 0)
            {
                await _productAppService.IncreaseStockAsync(id, quantidade);
            }
            else
            {
                await _productAppService.DecreaseStockAsync(id, quantidade);
            }

            return View("Index", await _productAppService.GetAllAsync());
        }

        private async Task<ProductViewModel> FillCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategoriesAsync();
            return product;
        }
    }
}