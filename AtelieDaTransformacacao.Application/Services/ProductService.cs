using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

/// <summary>
/// Serviço de produtos integrado com o gerador de links do WhatsApp e utilizando mapeamento manual de objetos.
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
        var dtos = new List<ProductDto>();

        foreach (var item in products)
        {
            dtos.Add(new ProductDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                Image = item.Image,
                CategoryId = item.CategoryId,
                StockQuantity = item.StockQuantity,
                IsAvailable = item.StockQuantity > 0,
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var item = await _productRepository.GetByIdAsync(id);
        if (item == null) return null;

        return new ProductDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Price = item.Price,
            Image = item.Image,
            CategoryId = item.CategoryId,
            StockQuantity = item.StockQuantity,
            IsAvailable = item.StockQuantity > 0,
            WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
        };
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.GetByCategoryAsync(categoryId);
        var dtos = new List<ProductDto>();

        foreach (var item in products)
        {
            dtos.Add(new ProductDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                Image = item.Image,
                CategoryId = item.CategoryId,
                StockQuantity = item.StockQuantity,
                IsAvailable = item.StockQuantity > 0,
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    public async Task<IEnumerable<ProductDto>> GetFeaturedAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var dtos = new List<ProductDto>();

        foreach (var item in products)
        {
            dtos.Add(new ProductDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                Image = item.Image,
                CategoryId = item.CategoryId,
                StockQuantity = item.StockQuantity,
                IsAvailable = item.StockQuantity > 0,
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    public async Task<int> CountAsync()
    {
        return await _productRepository.CountAsync();
    }

    public async Task<bool> DebitStockAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.StockQuantity < quantity)
            return false;

        product.StockQuantity -= quantity;
        await _productRepository.UpdateAsync(product);
        return true;
    }

    public async Task AddAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Title = productDto.Title,
            Description = productDto.Description,
            Price = productDto.Price,
            Image = productDto.Image,
            CategoryId = productDto.CategoryId,
            StockQuantity = productDto.StockQuantity
        };
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(productDto.Id);
        if (product == null) throw new Exception("Product not found");

        product.Title = productDto.Title;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.Image = productDto.Image;
        product.CategoryId = productDto.CategoryId;
        product.StockQuantity = productDto.StockQuantity;

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}