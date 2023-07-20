using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;

        public ProductsWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }
        //GET api/products/GetproductsWithCategory
        //[HttpGet("GetProductsWithCategory")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productServiceWithDto.GetProductsWithCategory());
        }

        //GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            
            return CreateActionResult(await _productServiceWithDto.GetAllAsync());
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))] //Metoda girmeden çalışır.
        //GET api/products/5
        [HttpGet("{id}")] //Eğer bu kısımda süslü paranterzlerle id  belirtilmezse queryString olarak bekler. Süslü parantez ile belirtilirse / koyularak id girilebilir.
        public async Task<IActionResult> GetById(int id)
        {
            
            return CreateActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            return CreateActionResult(await _productServiceWithDto.AddAsync(productDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            return CreateActionResult(await _productServiceWithDto.UpdateAsync(productDto));
        }
        //GET api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveAsync(id));
        }
        [HttpPost("SaveAll")]
        public async Task<IActionResult> Save(List<ProductDto> productDtos)
        {
            return CreateActionResult(await _productServiceWithDto.AddRangeAsync(productDtos));
        }
        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> Remove(List<int> ids)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveRangeAsync(ids));
        }
        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _productServiceWithDto.AnyAsync(x => x.Id == id));
        }
    }
}
