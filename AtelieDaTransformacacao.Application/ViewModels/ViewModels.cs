using System.Collections.Generic;
using System.Linq;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.ViewModels
{
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
        public int TotalItensEmEstoque => Produtos.Count(p => p.IsAvailable);
    }

    public class ProductFormViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // 🛠️ RESTAURADO: Mantido para corrigir os erros no Controller e nas Views
        public int ReleaseYear { get; set; }

        // 🛠️ RESTAURADO: Mantido para corrigir os erros de imagem no formulário
        public string CoverImageUrl { get; set; } = string.Empty;

        // 🛠️ ADICIONADO: Campo necessário para o mapeamento da imagem real do produto
        public string Image { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsAvailable { get; set; }

        // 🛠️ RESTAURADO: Controla a quantidade física no painel
        public int StockQuantity { get; set; }

        public IEnumerable<ProductCategoryDto> Categories { get; set; } = new List<ProductCategoryDto>();
    }
}