using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs
{

    /// <summary>
    /// Objeto de transferência de dados para as categorias de produtos do ateliê.
    /// </summary>
    public class ProductCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
    /// <summary>
    /// Dto para criação de uma nova categoria
    /// </summary>
    public class CreateProductCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para atualizar categoria
    /// </summary>
    public class UpdateProductCategoryDto 
    {
        public string Name { get; set;  } = string.Empty;
    }
}