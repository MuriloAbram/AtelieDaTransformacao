using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Domain.Entities
{
    /// <summary>
    /// BASE
    /// Entidade de Produtos
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Data de criação do registro no BD
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ProductCategory? Category { get; set; }
    }
}
