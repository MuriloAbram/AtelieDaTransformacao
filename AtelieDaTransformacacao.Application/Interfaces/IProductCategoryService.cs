using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de categorias.
    /// </summary>
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<ProductCategoryDto?> GetByIdAsync(int id);
        Task<ProductCategoryDto> CreateAsync(ProductCategoryDto categoryDto);
        Task UpdateAsync(ProductCategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}