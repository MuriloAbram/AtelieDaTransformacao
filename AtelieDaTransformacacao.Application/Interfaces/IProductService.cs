using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para serviços e operações lógicas de Produtos acabados.
    /// </summary>
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id); // Recupera um produto pelo seu ID.
        Task<IEnumerable<ProductDto>> GetAllAsync(); // Recupera todos os produtos cadastrados, retornando uma coleção de DTOs para consumo em camadas superiores ou interfaces de usuário.
        Task AddAsync(ProductDto productDto); // Adiciona um novo produto ao sistema, recebendo um DTO com os dados do produto a ser criado, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(ProductDto productDto); // Atualiza os dados de um produto existente, recebendo um DTO com as informações atualizadas do produto, realizando as validações necessárias e persistindo as alterações no banco de dados.
        Task RemoveAsync(int id); // Remove um produto do sistema com base no seu ID, realizando a exclusão lógica ou física conforme a implementação, garantindo a integridade dos dados e o histórico de registros relacionados ao produto.
    }
}