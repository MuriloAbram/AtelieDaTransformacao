using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers
{
    /// <summary>
    /// Controller protegida que permite ao administrador gerir (Criar, Editar, Eliminar) o catálogo de produtos e categorias.
    /// </summary>
    [Authorize(Roles = "Admin")] // Protegido apenas para administradores
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
        /// Lista todos os produtos e categorias agrupados dentro da Dashboard Administrativa.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Busca as duas listas em simultâneo do banco de dados
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();

            // Monta o ViewModel unificado esperado pela nova View elegante
            var dashboardViewModel = new DashboardViewModel
            {
                Produtos = products ?? new List<ProductDto>(),
                Categorias = categories ?? new List<ProductCategoryDto>()
            };

            return View(dashboardViewModel);
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
                IsFeatured = true // Definido como bool conforme sua alteração anterior
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
                IsFeatured = product.IsFeatured,
                Categories = categories ?? new List<ProductCategoryDto>()
            };

            // Passamos dados que não estão na ViewModel via ViewData/ViewBag de forma segura
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
                IsFeatured = true
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
            return View();
        }

        /// <summary>
        /// Grava a nova categoria na base de dados vinda do formulário/modal da Dashboard.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Nome da categoria inválido.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Instancia o ProductCategoryDto esperado pela camada de serviço do banco
                var categoryDto = new ProductCategoryDto
                {
                    Name = model.Name
                };

                await _categoryService.AddAsync(categoryDto);
                TempData["Success"] = "Categoria adicionada com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao salvar categoria: " + ex.Message;
            }

            // Redireciona de volta para a listagem principal do Admin atualizando os dados
            return RedirectToAction(nameof(Index));
        }
    }
}