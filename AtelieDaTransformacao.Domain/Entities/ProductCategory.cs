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

        public virtual ICollection<Product> Product { get; set} = new List<Product>();
    }
}
