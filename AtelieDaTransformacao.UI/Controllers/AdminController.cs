using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;

    public AdminController(
        IProductService productService,
        IProductCategoryService categoryService)
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
            IsAvailable = true,
            IsFeatured = false,
            StockQuantity = 1
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(
    ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }

        try
        {
            var product = new ProductDto
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,

                Image = string.IsNullOrWhiteSpace(model.Image)
                    ? model.CoverImageUrl
                    : model.Image,

                CategoryId = model.CategoryId,
                IsFeatured = model.IsFeatured,

                StockQuantity = model.IsAvailable
                    ? model.StockQuantity
                    : 0
            };

            await _productService.AddAsync(product);

            TempData["SuccessMessage"] =
                "Peça cadastrada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product =
            await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        var categories =
            await _categoryService.GetAllAsync();

        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            CoverImageUrl = product.Image,
            Image = product.Image,
            CategoryId = product.CategoryId,
            Price = product.Price,
            IsFeatured = product.IsFeatured,
            IsAvailable = product.IsAvailable,
            StockQuantity = product.StockQuantity,
            Categories =
                categories ??
                new List<ProductCategoryDto>()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProducts(
    ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }

        try
        {
            var product = new ProductDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,

                Image = string.IsNullOrWhiteSpace(model.Image)
                    ? model.CoverImageUrl
                    : model.Image,

                CategoryId = model.CategoryId,
                IsFeatured = model.IsFeatured,

                StockQuantity = model.IsAvailable
                    ? model.StockQuantity
                    : 0
            };

            await _productService.UpdateAsync(product);

            TempData["SuccessMessage"] =
                "Peça atualizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(
    ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }

        try
        {
            var product = new ProductDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Image = string.IsNullOrWhiteSpace(model.Image)
                    ? model.CoverImageUrl
                    : model.Image,
                CategoryId = model.CategoryId,
                Price = model.Price,
                IsFeatured = model.IsFeatured,
                StockQuantity = model.IsAvailable
                    ? model.StockQuantity
                    : 0
            };

            await _productService.UpdateAsync(product);

            TempData["SuccessMessage"] =
                "Peça atualizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            model.Categories =
                await _categoryService.GetAllAsync();

            return View(model);
        }
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(
            new CreateProductCategoryDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(
        CreateProductCategoryDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _categoryService.AddAsync(model);

            TempData["SuccessMessage"] =
                "Categoria criada com sucesso!";

            return RedirectToAction(
                nameof(Categories));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Categories()
    {
        var categories =
            await _categoryService.GetAllAsync();

        return View(categories);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(
        int id)
    {
        try
        {
            var deleted =
                await _categoryService.DeleteAsync(id);

            if (!deleted)
            {
                TempData["ErrorMessage"] =
                    "Categoria não encontrada.";
            }
            else
            {
                TempData["SuccessMessage"] =
                    "Categoria removida com sucesso!";
            }
        }
        catch
        {
            TempData["ErrorMessage"] =
                "Não é possível apagar esta categoria porque existem produtos associados a ela.";
        }

        return RedirectToAction(
            nameof(Categories));
    }
}