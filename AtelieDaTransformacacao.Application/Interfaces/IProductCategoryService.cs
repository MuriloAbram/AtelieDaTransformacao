using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de categorias de produtos.
    /// </summary>
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<ProductCategoryDto?> GetByIdAsync(int id);

        // Recebe DTO de criação
        Task AddAsync(CreateProductCategoryDto createDto);

        // Mantém assinatura existente para update (pode alterar para UpdateProductCategoryDto se desejar)
        Task UpdateAsync(ProductCategoryDto categoryDto);

        // Retorna bool indicando sucesso/falha
        Task<bool> DeleteAsync(int id);
    }
}