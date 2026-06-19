using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller protegida que permite ao administrador gerir (Criar, Editar, Eliminar) o catálogo de produtos.
/// </summary>
[Authorize]
public class AdminController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;

    public AdminController(IProductService productService, IProductCategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Lista todos os produtos cadastrados num formato de tabela de gestão administrativa.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    /// <summary>
    /// Exibe o formulário de criação de um novo produto.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.Categories = await _categoryService.GetAllAsync();
        return View();
    }

    /// <summary>
    /// Grava o novo produto criado na base de dados.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductDto model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        await _productService.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Exibe o formulário de edição de um produto existente carregado através do ID.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = await _categoryService.GetAllAsync();
        return View(product);
    }

    /// <summary>
    /// Atualiza as informações do produto editado na base de dados.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductDto model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        await _productService.UpdateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Remove um produto do catálogo através do ID fornecido.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}