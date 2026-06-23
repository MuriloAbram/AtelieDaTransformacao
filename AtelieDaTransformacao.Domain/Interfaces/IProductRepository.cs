using AtelieDaTransformacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Domain.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Faz busca e retorna Pordutos 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetFeaturedAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

        /// <summary>
        /// CRUD = cria, atualiza, deleta e conta os produtos cadastrados.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
