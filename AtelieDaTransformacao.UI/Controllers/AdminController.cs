using System;
using System.Linq;
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
    /// Exibe o formulário de criação de um novo produto usando a ViewModel correta.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var categories = await _categoryService.GetAllAsync();

        var viewModel = new ProductFormViewModel
        {
            Categories = categories ?? new List<ProductCategoryDto>(), // Previne o NullReference na View
            ReleaseYear = DateTime.Now.Year
        };

        return View(viewModel);
    }

    /// <summary>
    /// Grava o novo produto criado na base de dados fazendo a conversão da ViewModel para DTO.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductFormViewModel model, decimal price, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            // Se houver erro, recarrega a lista obrigatoriamente dentro do objeto para a View não quebrar
            var categories = await _categoryService.GetAllAsync();
            model.Categories = categories ?? new List<ProductCategoryDto>();
            return View(model);
        }

        // Mapeia a ViewModel + os campos extras do formulário para o seu DTO de persistência
        var productDto = new ProductDto
        {
            Title = model.Title,
            Description = model.Description,
            Image = model.CoverImageUrl,
            CategoryId = model.CategoryId,
            Price = price,
            IsAvailable = isAvailable,
            IsFeatured = model.IsFeatured.ToString()
        };

        await _productService.AddAsync(productDto);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Exibe o formulário de edição convertendo o DTO do banco para a ViewModel da View.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();

        var categories = await _categoryService.GetAllAsync();

        // Converte o DTO retornado do serviço para o formato esperado pela View de Edição
        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            CoverImageUrl = product.Image,
            CategoryId = product.CategoryId,
            IsFeatured = product.IsFeatured == "true" || product.IsFeatured == "True",
            Categories = categories ?? new List<ProductCategoryDto>()
        };

        // Passamos dados que não estão na ViewModel via ViewData/ViewBag de forma segura apenas para exibição inicial se necessário
        ViewBag.CurrentPrice = product.Price;
        ViewBag.CurrentAvailable = product.IsAvailable;

        return View(viewModel);
    }

    /// <summary>
    /// Atualiza as informações do produto editado na base de dados.
    /// </summary>
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
            IsFeatured = model.IsFeatured.ToString()
        };

        await _productService.UpdateAsync(productDto);
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

    /// <summary>
    /// Exibe o formulário para criação de uma nova categoria de produto.
    /// </summary>
    [HttpGet]
    public IActionResult CreateCategory()
    {
        // Retorna a View vazia para cadastro
        return View();
    }

    /// <summary>
    /// Grava a nova categoria na base de dados.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(ProductCategoryDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Altere para o método correto do seu _categoryService (Ex: AddAsync, CreateAsync, etc.)
        await _categoryService.AddAsync(model);

        // Redireciona de volta para a listagem principal do Admin
        return RedirectToAction(nameof(Index));
    }
}