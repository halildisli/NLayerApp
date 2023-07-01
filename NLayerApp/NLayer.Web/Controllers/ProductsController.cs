using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Filters;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsApiService _productsApiService;
        private readonly CategoriesApiService _categoriesApiService;

        public ProductsController(ProductsApiService productsApiService, CategoriesApiService categoriesApiService)
        {
            _productsApiService = productsApiService;
            _categoriesApiService = categoriesApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productsApiService.GetProductWithCategoriesAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoriesApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                await _productsApiService.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoriesApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [HttpGet]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var productDto = await _productsApiService.GetByIdAsync(id);
            var categoriesDto = await _categoriesApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productsApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoriesApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _productsApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
