using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller protegida que permite ao administrador gerir (Criar, Editar, Eliminar) o catálogo de produtos e categorias.
/// </summary>
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;

    public AdminController(IProductService productService, IProductCategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var categories = await _categoryService.GetAllAsync();

        var viewModel = new ProductFormViewModel
        {
            Categories = categories ?? new List<ProductCategoryDto>(),
            ReleaseYear = DateTime.Now.Year
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductFormViewModel model, decimal price, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            model.Categories = categories ?? new List<ProductCategoryDto>();
            return View(model);
        }

        var productDto = new ProductDto
        {
            Title = model.Title,
            Description = model.Description,
            Image = model.CoverImageUrl,
            CategoryId = model.CategoryId,
            Price = price,
            IsAvailable = isAvailable,
            IsFeatured = true,
            StockQuantity = model.StockQuantity // 👈 Persistência mapeada
        };

        await _productService.AddAsync(productDto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        var categories = await _categoryService.GetAllAsync();

        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            CoverImageUrl = product.Image,
            CategoryId = product.CategoryId,
            IsFeatured = true,
            StockQuantity = product.StockQuantity, // 👈 Carrega do banco
            Categories = categories ?? new List<ProductCategoryDto>()
        };

        ViewBag.CurrentPrice = product.Price;
        ViewBag.CurrentAvailable = product.IsAvailable;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductFormViewModel model, decimal price, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            model.Categories = categories ?? new List<ProductCategoryDto>();
            return View(model);
        }

        var productDto = new ProductDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Image = model.CoverImageUrl,
            CategoryId = model.CategoryId,
            Price = price,
            IsAvailable = isAvailable,
            IsFeatured = true,
            StockQuantity = model.StockQuantity // 👈 Salva modificação
        };

        await _productService.UpdateAsync(productDto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new CreateProductCategoryDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var categoryDto = new ProductCategoryDto
        {
            Name = model.Name
        };

        await _categoryService.AddAsync(categoryDto);
        return RedirectToAction(nameof(Index));
    }

    // =====================================================================
    // NOVOS MÉTODOS ADICIONADOS PARA AS CATEGORIAS NO DASHBOARD
    // =====================================================================

    /// <summary>
    /// Lista todas as categorias cadastradas na View correspondente.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Categories()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    /// <summary>
    /// Remove uma categoria do sistema tratando erros de dependência de chaves.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Categoria removida com sucesso!";
        }
        catch (Exception)
        {
            // Impede a quebra caso a categoria tenha chaves vinculadas a produtos
            TempData["ErrorMessage"] = "Não é possível apagar esta categoria porque existem produtos associados a ela.";
        }

        return RedirectToAction(nameof(Categories));
    }
}