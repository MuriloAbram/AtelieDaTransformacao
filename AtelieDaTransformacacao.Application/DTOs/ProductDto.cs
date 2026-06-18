using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados para o Produto finalizado.
    /// Concentra precificação, controle de estoque e chaves de relacionamento com o DER.
    /// </summary>
    public class ProductDto
    {
        public int IdProduct { get; set; } // Chave primária do produto
        public string ProductName { get; set; } = string.Empty; // Nome do produto, utilizado para identificação e exibição em interfaces de usuário
        public string Description { get; set; } = string.Empty; // Descrição detalhada do produto, incluindo características, funcionalidades e informações relevantes para o cliente
        public decimal SalePrice { get; set; } // Preço de venda do produto, utilizado para cálculos de faturamento e exibição em catálogos
        public int StockQuantity { get; set; } // Quantidade em estoque do produto, utilizada para controle de inventário e disponibilidade para venda
        public int IdCategory { get; set; } // Chave estrangeira para a categoria do produto, vinculando-o à tabela de categorias para classificação e organização
        public int IdSupplier { get; set; } // Chave estrangeira para o fornecedor do produto, vinculando-o à tabela de fornecedores para gestão de compras e relacionamento comercial
        public int IdMaterial { get; set; } // Chave estrangeira para o material utilizado na fabricação do produto, vinculando-o à tabela de materiais para controle de produção e custos
    }
}