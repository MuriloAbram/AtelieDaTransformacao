using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs
{

    /// <summary>
    /// DTO completo de produto, ideal para exibição em listagens ou páginas de detalhes.
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(180)]
        public string Title { get; set; } = string.Empty;

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100000.00)]
        public decimal Price { get; set; }

        public string Image { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        /// <summary>
        /// Armazena o link gerado dinamicamente para direcionar o cliente ao WhatsApp deste produto específico.
        /// </summary>
        public string WhatsAppLink { get; set; } = string.Empty;
    }

    public class CreateProductDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string IsFeatured { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string IsFeatured { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}