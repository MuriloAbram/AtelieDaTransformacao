using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para rastreamento de Ordens de Produção industriais/manuais.
    /// </summary>
    public interface IProductionOrderService
    {
        Task<ProductionOrderDto> GetByIdAsync(int id); // Rastreia uma ordem de produção específica por ID.
        Task<IEnumerable<ProductionOrderDto>> GetAllAsync(); // Rastreia todas as ordens de produção cadastradas, retornando uma coleção de DTOs para consumo em camadas superiores ou interfaces de usuário.
        Task AddAsync(ProductionOrderDto productionOrderDto); // Rastreia a adição de uma nova ordem de produção ao sistema, recebendo um DTO com os dados da ordem a ser criada, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(ProductionOrderDto productionOrderDto); // Rastreia a atualização de uma ordem de produção existente, recebendo um DTO com os dados atualizados da ordem, realizando as validações necessárias e persistindo as alterações no banco de dados.
    }
}