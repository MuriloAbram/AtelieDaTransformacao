using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Interface para o serviço de controle e exibição de produtos do catálogo.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetFeaturedAsync();
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);
    /// <summary>
    /// CRUD - adiciona, deleta, atualiza e conta Produtos existentes
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    Task AddAsync(ProductDto productDto);
    Task UpdateAsync(ProductDto productDto);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}