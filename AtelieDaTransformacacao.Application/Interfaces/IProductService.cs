using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de produtos e anúncios.
    /// </summary>
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(); // Retorna todos os produtos disponíveis.
        Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId); // Retorna os produtos filtrados por categoria, recebendo o ID da categoria como parâmetro.
        Task<IEnumerable<ProductDto>> GetByUserIdAsync(string userId); // Retorna os produtos filtrados por usuário/vendedor, recebendo o ID do usuário como parâmetro.
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task DeleteAsync(int id);
    }
}