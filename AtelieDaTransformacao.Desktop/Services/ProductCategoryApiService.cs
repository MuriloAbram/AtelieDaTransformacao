using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Desktop.Services
{
    // =============================================================================
    // AtelieDaTransformacao.Desktop - Services/ProductCategoryApiService.cs
    // =============================================================================
    // CONCEITO: Service de Categorias (Product Categories)
    //
    // Realiza operações CRUD de categorias via API REST:
    //   GET    /api/categories         Listar todas as categorias
    //   POST   /api/categories         Criar categoria (requer Admin)
    //   PUT    /api/categories/{id}    Atualizar categoria (requer Admin)
    //   DELETE /api/categories/{id}    Excluir categoria (requer Admin)
    // =============================================================================

    public class ProductCategoryApiService
    {
        private readonly HttpClientHelper _http;

        public ProductCategoryApiService()
        {
            _http = HttpClientHelper.Instance;
        }

        /// <summary>
        /// Lista todas as categorias via GET /api/categories
        /// </summary>
        public async Task<List<ProductCategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _http.GetAsync<List<ProductCategoryDto>>("/api/categories");
                return categories ?? new List<ProductCategoryDto>();
            }
            catch
            {
                return new List<ProductCategoryDto>();
            }
        }

        /// <summary>
        /// Cria uma categoria via POST /api/categories
        /// </summary>
        public async Task<(bool Success, ProductCategoryDto? Category, string ErrorMessage)> CreateAsync(CreateProductCategoryDto dto)
        {
            var result = await _http.PostAsync<ProductCategoryDto>("/api/categories", dto);
            return (result.Success, result.Data, result.ErrorMessage);
        }

        /// <summary>
        /// Atualiza uma categoria via PUT /api/categories/{id}
        /// </summary>
        public async Task<(bool Success, ProductCategoryDto? Category, string ErrorMessage)> UpdateAsync(int id, UpdateProductCategoryDto dto)
        {
            var result = await _http.PutAsync<ProductCategoryDto>($"/api/categories/{id}", dto);
            return (result.Success, result.Data, result.ErrorMessage);
        }

        /// <summary>
        /// Exclui uma categoria via DELETE /api/categories/{id}
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"/api/categories/{id}");
        }
    }
}