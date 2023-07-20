using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryServiceWithDto : ServiceWithDto<Category, CategoryDto>, ICategoryServiceWithDto
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceWithDto(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var entity = await _categoryRepository.GetByIdAsync(categoryId);
            var entityDto = _mapper.Map<CategoryWithProductsDto>(entity);
            return CustomResponseDto<CategoryWithProductsDto>.Success(StatusCodes.Status200OK, entityDto);
        }
    }
}
