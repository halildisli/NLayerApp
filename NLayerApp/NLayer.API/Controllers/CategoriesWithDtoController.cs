using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesWithDtoController : CustomBaseController
    {
        private readonly ICategoryServiceWithDto _categoryServiceWithDto;

        public CategoriesWithDtoController(ICategoryServiceWithDto categoryServiceWithDto)
        {
            _categoryServiceWithDto = categoryServiceWithDto;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryServiceWithDto.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto categoryDto)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddAsync(categoryDto));
        }
    }
}
