using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryService(
            IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var dtos = new List<ProductCategoryDto>();

            foreach (var item in categories)
            {
                dtos.Add(new ProductCategoryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                });
            }

            return dtos;
        }

        public async Task<ProductCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new ProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task AddAsync(CreateProductCategoryDto categoryDto)
        {
            var category = new ProductCategory
            {
                Name = categoryDto.Name,
            };

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(ProductCategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);

            if (category == null)
                throw new Exception("Categoria não encontrada.");

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(id);

            return true;
        }
    }
}