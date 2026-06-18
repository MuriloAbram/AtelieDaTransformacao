using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para gerenciamento e fluxos operacionais de Pedidos de Venda.
    /// </summary>
    public interface IOrderService
    {
        Task<OrderDto> GetByIdAsync(int id); // Método para obter um pedido por seu ID.
        Task<IEnumerable<OrderDto>> GetAllAsync(); // Método para obter todos os pedidos cadastrados, retornando uma coleção de DTOs para consumo em camadas superiores ou interfaces de usuário.
        Task AddAsync(OrderDto orderDto); // Método para adicionar um novo pedido ao sistema, recebendo um DTO com os dados do pedido a ser criado, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(OrderDto orderDto); // Método para atualizar os dados de um pedido existente, recebendo um DTO com as informações atualizadas do pedido, realizando as validações necessárias e persistindo as alterações no banco de dados.
    }
}