using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para serviços e operações lógicas de Categorias.
    /// </summary>
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id); // Lê uma categoria específica por ID.
        Task<IEnumerable<CategoryDto>> GetAllAsync(); // Lê todas as categorias, retornando uma coleção de objetos CategoryDto.
        Task AddAsync(CategoryDto categoryDto); // Cria uma nova categoria, recebendo um objeto CategoryDto com os dados da categoria a ser adicionada.
        Task UpdateAsync(CategoryDto categoryDto); // Atualiza uma categoria existente, recebendo um objeto CategoryDto com os dados atualizados da categoria.
        Task RemoveAsync(int id); // Exclui uma categoria específica por ID, removendo-a do sistema.
    }
}