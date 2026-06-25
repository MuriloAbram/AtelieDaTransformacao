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
/// Controller protegida que permite ao administrador gerir (Criar, Editar, Eliminar) o catálogo de produtos.
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
}