using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Desktop.Services
{
    // =============================================================================
    // AtelieDaTransformacao.Desktop - Services/ProductsApiService.cs
    // =============================================================================
    // CONCEITO: Service de Products
    //
    // Realiza todas as operações CRUD de produtos via API REST:
    //   GET    /api/products         Listar todos os produtos
    //   GET    /api/products/{id}    Buscar produto por ID
    //   POST   /api/products         Criar produto (requer Admin)
    //   PUT    /api/products/{id}    Atualizar produto (requer Admin)
    //   DELETE /api/products/{id}    Excluir produto (requer Admin)
    //
    // IMPORTANTE: A autorização é verificada pela API; o Desktop apenas
    // executa as chamadas e controla a interface.
    // =============================================================================

    public class ProductsApiService
    {
        private readonly HttpClientHelper _http;

        public ProductsApiService()
        {
            _http = HttpClientHelper.Instance;
        }

        /// <summary>
        /// Lista todos os produtos via GET /api/products
        /// </summary>
        public async Task<List<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _http.GetAsync<List<ProductDto>>("/api/products");
                return products ?? new List<ProductDto>();
            }
            catch
            {
                return new List<ProductDto>();
            }
        }

        /// <summary>
        /// Busca um produto específico por ID via GET /api/products/{id}
        /// </summary>
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            return await _http.GetAsync<ProductDto>($"/api/products/{id}");
        }

        /// <summary>
        /// Cria um novo produto via POST /api/products.
        /// </summary>
        public async Task<(bool Success, ProductDto? Product, string ErrorMessage)> CreateAsync(CreateProductDto dto)
        {
            var result = await _http.PostAsync<ProductDto>("/api/products", dto);
            return (result.Success, result.Data, result.ErrorMessage);
        }

        /// <summary>
        /// Atualiza um produto existente via PUT /api/products/{id}.
        /// </summary>
        public async Task<(bool Success, ProductDto? Product, string ErrorMessage)> UpdateAsync(int id, UpdateProductDto dto)
        {
            var result = await _http.PutAsync<ProductDto>($"/api/products/{id}", dto);
            return (result.Success, result.Data, result.ErrorMessage);
        }

        /// <summary>
        /// Exclui um produto via DELETE /api/products/{id}.
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            return await _http.DeleteAsync($"/api/products/{id}");
        }
    }
}