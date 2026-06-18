using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para serviços e operações lógicas de Funcionários.
    /// </summary>
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetByIdAsync(int id); // Lê um funcionário específico pelo ID.
        Task<IEnumerable<EmployeeDto>> GetAllAsync(); // Lê todos os funcionários cadastrados, retornando uma coleção de DTOs para consumo em camadas superiores ou interfaces de usuário.
        Task AddAsync(EmployeeDto employeeDto); // Adiciona um novo funcionário ao sistema, recebendo um DTO com os dados do funcionário a ser criado, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(EmployeeDto employeeDto); // Atualiza os dados de um funcionário existente, recebendo um DTO com as informações atualizadas do funcionário, realizando as validações necessárias e persistindo as alterações no banco de dados.
        Task RemoveAsync(int id); // Remove um funcionário do sistema com base no seu ID, realizando a exclusão lógica ou física conforme a implementação, garantindo a integridade dos dados e o histórico de registros relacionados ao funcionário.
    }
}