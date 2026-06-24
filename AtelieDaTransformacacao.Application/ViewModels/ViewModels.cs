using System.Collections.Generic;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.ViewModels;

/// <summary>
/// ViewModel unificada para alimentar as Views Razor da interface do usuário (UI), agrupando produtos e filtros por categorias.
/// </summary>
public class HomeViewModel
{
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public IEnumerable<ProductCategoryDto> Categories { get; set; } = new List<ProductCategoryDto>();
    public int? SelectedCategoryId { get; set; }
}

public class DashboardViewModel
{
    public IEnumerable<ProductDto> Produtos { get; set; } = new List<ProductDto>();
    public IEnumerable<ProductCategoryDto> Categorias { get; set; } = new List<ProductCategoryDto>();

    public int TotalProdutos => Produtos.Count();
    public int ProdutosDestaque => Produtos.Count(p => p.IsFeatured);
}
public class ProductFormViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public bool IsFeatured { get; set; }

    public IEnumerable<ProductCategoryDto> Categories { get; set; } = new List<ProductCategoryDto>();
}