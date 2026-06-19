using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Interface para o serviço de gerenciamento de categorias de produtos (Tábuas, Mesas, etc).
/// </summary>
public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
    Task<ProductCategoryDto?> GetByIdAsync(int id);
    Task AddAsync(ProductCategoryDto categoryDto);
    Task UpdateAsync(ProductCategoryDto categoryDto);
    Task DeleteAsync(int id);
}