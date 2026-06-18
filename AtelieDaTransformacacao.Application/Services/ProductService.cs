using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services
{
    /// <summary>
    /// Implementação do serviço de regras de negócio para produtos com mapeamento manual e injeção do link de redirecionamento.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWhatsAppService _whatsAppService;

        public ProductService(IProductRepository productRepository, IWhatsAppService whatsAppService)
        {
            _productRepository = productRepository;
            _whatsAppService = whatsAppService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                UserId = p.UserId,
                SellerName = p.User?.FullName ?? string.Empty,
                WhatsAppUrl = p.User != null
                    ? _whatsAppService.GenerateRedirectUrl(p.User.WhatsAppNumber, p.Title, p.Price)
                    : string.Empty
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                UserId = product.UserId,
                SellerName = product.User?.FullName ?? string.Empty,
                WhatsAppUrl = product.User != null
                    ? _whatsAppService.GenerateRedirectUrl(product.User.WhatsAppNumber, product.Title, product.Price)
                    : string.Empty
            };
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                UserId = p.UserId,
                SellerName = p.User?.FullName ?? string.Empty,
                WhatsAppUrl = p.User != null
                    ? _whatsAppService.GenerateRedirectUrl(p.User.WhatsAppNumber, p.Title, p.Price)
                    : string.Empty
            }).ToList();
        }

        public async Task<IEnumerable<ProductDto>> GetByUserIdAsync(string userId)
        {
            var products = await _productRepository.GetByUserIdAsync(userId);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                UserId = p.UserId,
                SellerName = p.User?.FullName ?? string.Empty,
                WhatsAppUrl = p.User != null
                    ? _whatsAppService.GenerateRedirectUrl(p.User.WhatsAppNumber, p.Title, p.Price)
                    : string.Empty
            }).ToList();
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl,
                CategoryId = productDto.CategoryId,
                UserId = productDto.UserId
            };

            var created = await _productRepository.CreateAsync(product);

            return new ProductDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Price = created.Price,
                ImageUrl = created.ImageUrl,
                CategoryId = created.CategoryId,
                UserId = created.UserId
            };
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl,
                CategoryId = productDto.CategoryId,
                UserId = productDto.UserId
            };

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}