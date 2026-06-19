using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gerenciamento de categorias.
    /// </summary>
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync(); // Método para obter todas as categorias de produtos.
        Task<ProductCategoryDto?> GetByIdAsync(int id); // Método para obter uma categoria de produto específica pelo seu ID.
        Task<ProductCategoryDto> CreateAsync(ProductCategoryDto categoryDto); // Método para criar uma nova categoria de produto, recebendo um DTO com as informações da categoria a ser criada e retornando o DTO da categoria criada.
        Task UpdateAsync(ProductCategoryDto categoryDto); // Método para atualizar uma categoria de produto existente, recebendo um DTO com as informações atualizadas da categoria. O método não retorna nenhum valor, mas pode lançar exceções em caso de erros (ex: categoria não encontrada).
        Task DeleteAsync(int id); // Método para excluir uma categoria de produto pelo seu ID. O método não retorna nenhum valor, mas pode lançar exceções em caso de erros (ex: categoria não encontrada ou categoria associada a produtos existentes).
    }
}