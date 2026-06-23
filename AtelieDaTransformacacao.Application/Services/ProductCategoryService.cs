using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

/// <summary>
/// Service for managing product categories, performing manual mapping between DTOs and Entities.
/// </summary>
public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _categoryRepository;

    public ProductCategoryService(IProductCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Retorna todas as categorias cadastradas mapeadas manualmente para DTOs.
    /// </summary>
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

    /// <summary>
    /// Busca uma categoria pelo ID e realiza o mapeamento manual para DTO.
    /// </summary>
    public async Task<ProductCategoryDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new ProductCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    /// <summary>
    /// Converte o DTO recebido para a entidade ProductCategory e salva no banco de dados.
    /// </summary>
    public async Task AddAsync(ProductCategoryDto categoryDto)
    {
        var category = new ProductCategory
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };

        await _categoryRepository.AddAsync(category);
    }

    /// <summary>
    /// Atualiza as propriedades da categoria existente com base nos dados do DTO.
    /// </summary>
    public async Task UpdateAsync(ProductCategoryDto categoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
        if (category == null) throw new Exception("Category not found");

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;

        await _categoryRepository.UpdateAsync(category);
    }

    /// <summary>
    /// Remove uma categoria do banco de dados através do ID.
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }
}
