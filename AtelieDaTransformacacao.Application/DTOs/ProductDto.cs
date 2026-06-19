using System;
using System.Collections.Generic;
using System.Text;

namespace AtelieDaTransformacao.Application.DTOs
{
    /// <summary>
    /// Objeto de transferência de dados para produtos/anúncios.
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; } //  Identificador único do produto
        public string Title { get; set; } = string.Empty; // Título ou nome do produto
        public string Description { get; set; } = string.Empty; // Descrição detalhada do produto, incluindo características, estado de conservação, etc.
        public decimal Price { get; set; } // Preço do produto, formatado para exibição (ex: R$ 150,00)
        public string ImageUrl { get; set; } = string.Empty; // URL da imagem do produto, que pode ser usada para exibir a foto do anúncio
        public int CategoryId { get; set; } // Categoria do produto, representada pelo ID da categoria (ex: 1 para Roupas, 2 para Acessórios, etc.)
        public string CategoryName { get; set; } = string.Empty; // Categoria do produto, representada pelo nome da categoria (ex: "Roupas", "Acessórios", etc.)
        public string UserId { get; set; } = string.Empty; // Identificador do usuário/vendedor que criou o anúncio, representado pelo ID do usuário
        public string SellerName { get; set; } = string.Empty; // Nome do usuário/vendedor que criou o anúncio, representado pelo nome completo do usuário
        public string WhatsAppUrl { get; set; } = string.Empty; // URL de redirecionamento para o WhatsApp, gerada dinamicamente com base nas informações do produto e do vendedor, que pode ser usada para facilitar o contato entre comprador e vendedor
    }
}