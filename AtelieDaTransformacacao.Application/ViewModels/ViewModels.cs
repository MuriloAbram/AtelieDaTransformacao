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