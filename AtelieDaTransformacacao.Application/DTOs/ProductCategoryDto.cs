using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de transferência de dados para categorias de produtos.
    /// </summary>
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}