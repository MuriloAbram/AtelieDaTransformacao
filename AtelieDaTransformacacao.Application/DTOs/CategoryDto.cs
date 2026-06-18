using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para a entidade Categoria.
    /// Armazena as informações básicas de classificação dos produtos.
    /// </summary>
    public class CategoryDto
    {
        public int IdCategory { get; set; } // Identificador Category
        public string CategoryName { get; set; } = string.Empty; // Nome da Categoria, utilizado para classificação e organização dos produtos
    }
}