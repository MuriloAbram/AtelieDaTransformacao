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
        public int Id { get; set; } // Identificador único da categoria
        public string Name { get; set; } = string.Empty; // Nome da categoria
        public string Description { get; set; } = string.Empty; // Descrição detalhada da categoria
    }
}