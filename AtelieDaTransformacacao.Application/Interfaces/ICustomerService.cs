using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para serviços e operações lógicas de Clientes.
    /// </summary>
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(int id); // Recupera um cliente pelo seu ID.
        Task<IEnumerable<CustomerDto>> GetAllAsync(); // Recupera todos os clientes cadastrados.
        Task AddAsync(CustomerDto customerDto); // Adiciona um novo cliente ao sistema, recebendo um DTO com os dados do cliente a ser criado.
        Task UpdateAsync(CustomerDto customerDto); // Atualiza os dados de um cliente existente, recebendo um DTO com as informações atualizadas do cliente.
        Task RemoveAsync(int id); // Remove um cliente do sistema com base no seu ID, realizando a exclusão lógica ou física conforme a implementação.
    }
}