using AtelieDaTransformacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        /// <summary>
        /// Faz busca e retorna categoria
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory?> GetByIdAsync(int id);
        
        /// <summary>
        /// CRUD - Adiciona, atualiza, deleta(por id) e conta categorias cadastradas
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task AddAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
