using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de produtos e anúncios.
    /// </summary>
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<ProductDto>> GetByUserIdAsync(string userId);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task DeleteAsync(int id);
    }
}