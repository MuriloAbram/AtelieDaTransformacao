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
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string WhatsAppUrl { get; set; } = string.Empty;
    }
}