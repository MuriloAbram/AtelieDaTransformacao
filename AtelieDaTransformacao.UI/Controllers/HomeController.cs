using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller principal da loja, responsável por exibir a vitrine de produtos para os clientes.
/// </summary>
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;

    public HomeController(IProductService productService, IProductCategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Exibe a página inicial com todos os produtos e categorias disponíveis, permitindo filtros.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(int? categoryId)
    {
        var viewModel = new HomeViewModel
        {
            Categories = await _categoryService.GetAllAsync(),
            SelectedCategoryId = categoryId
        };

        if (categoryId.HasValue)
        {
            viewModel.Products = await _productService.GetByCategoryAsync(categoryId.Value);
        }
        else
        {
            viewModel.Products = await _productService.GetAllAsync();
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe os detalhes de um produto específico com o link direcionado para o WhatsApp.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ProductDetails(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}