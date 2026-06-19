using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs;

/// <summary>
/// DTO completo de produto, ideal para exibição em listagens ou páginas de detalhes.
/// </summary>
public class ProductDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000.00)]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }

    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Armazena o link gerado dinamicamente para direcionar o cliente ao WhatsApp deste produto específico.
    /// </summary>
    public string WhatsAppLink { get; set; } = string.Empty;
}