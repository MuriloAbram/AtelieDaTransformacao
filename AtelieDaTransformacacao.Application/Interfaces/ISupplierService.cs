using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para mapeamento e serviços relacionados a Fornecedores.
    /// </summary>
    public interface ISupplierService
    {
        Task<SupplierDto> GetByIdAsync(int id); // Método para obter um fornecedor por ID.
        Task<IEnumerable<SupplierDto>> GetAllAsync(); // Método para obter todos os fornecedores.
        Task AddAsync(SupplierDto supplierDto); // Método para adicionar um novo fornecedor, recebendo um DTO com os dados do fornecedor a ser criado, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(SupplierDto supplierDto); // Método para atualizar os dados de um fornecedor existente, recebendo um DTO com as informações atualizadas do fornecedor, realizando as validações necessárias e persistindo as alterações no banco de dados.
        Task RemoveAsync(int id); // Método para remover um fornecedor do sistema com base no seu ID, realizando a exclusão lógica ou física conforme a implementação, garantindo a integridade dos dados e o histórico de registros relacionados ao fornecedor.
    }
}