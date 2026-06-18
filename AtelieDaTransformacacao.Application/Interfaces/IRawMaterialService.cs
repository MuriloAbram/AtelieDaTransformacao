using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces
{
    /// <summary>
    /// Interface de contrato para controle de Matérias-Primas do estoque físico.
    /// </summary>
    public interface IRawMaterialService
    {
        Task<RawMaterialDto> GetByIdAsync(int id); // Método para obter uma matéria-prima por seu ID.
        Task<IEnumerable<RawMaterialDto>> GetAllAsync(); // Método para obter todas as matérias-primas cadastradas, retornando uma coleção de DTOs para consumo em camadas superiores ou interfaces de usuário.
        Task AddAsync(RawMaterialDto rawMaterialDto); // Método para adicionar uma nova matéria-prima ao sistema, recebendo um DTO com os dados da matéria-prima a ser criada, realizando as validações necessárias e persistindo as informações no banco de dados.
        Task UpdateAsync(RawMaterialDto rawMaterialDto); // Método para atualizar os dados de uma matéria-prima existente, recebendo um DTO com as informações atualizadas da matéria-prima, realizando as validações necessárias e persistindo as alterações no banco de dados.
        Task RemoveAsync(int id); // Método para remover uma matéria-prima do sistema com base no seu ID, realizando a exclusão lógica ou física conforme a implementação, garantindo a integridade dos dados e o histórico de registros relacionados à matéria-prima.
    }
}