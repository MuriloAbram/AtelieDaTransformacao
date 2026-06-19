using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Domain.Entities
{
    /// <summary>
    /// BASE
    /// Entidade da Categoria do Produto 
    /// </summary>
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Lista os produtos que pertencem a categoria
        /// </summary>
        public virtual ICollection<Product> Product { get; set} = new List<Product>();
    }
}
