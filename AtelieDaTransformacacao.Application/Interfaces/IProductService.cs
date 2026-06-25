using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Interface para o serviço de controle e exibição de produtos do catálogo.
/// </summary>
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetFeaturedAsync();
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);

    /// <summary>
    /// Realiza a baixa de estoque quando uma venda é aprovada. Retorna falso se não houver saldo suficiente.
    /// </summary>
    Task<bool> DebitStockAsync(int productId, int quantity);

    Task AddAsync(ProductDto productDto);
    Task UpdateAsync(ProductDto productDto);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}