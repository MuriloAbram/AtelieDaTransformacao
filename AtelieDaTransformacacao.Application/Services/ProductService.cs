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
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable,
                ProductCategoryId = item.ProductCategoryId,
                CategoryName = item.ProductCategory?.Name ?? string.Empty,
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Name, item.Price)
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
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            ImageUrl = item.ImageUrl,
            IsAvailable = item.IsAvailable,
            ProductCategoryId = item.ProductCategoryId,
            CategoryName = item.ProductCategory?.Name ?? string.Empty,
            WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Name, item.Price)
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
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable,
                ProductCategoryId = item.ProductCategoryId,
                CategoryName = item.ProductCategory?.Name ?? string.Empty,
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Name, item.Price)
            });
        }
        return dtos;
    }

    public async Task AddAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            ImageUrl = productDto.ImageUrl,
            IsAvailable = productDto.IsAvailable,
            ProductCategoryId = productDto.ProductCategoryId
        };
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(productDto.Id);
        if (product == null) throw new Exception("Product not found");

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.ImageUrl = productDto.ImageUrl;
        product.IsAvailable = productDto.IsAvailable;
        product.ProductCategoryId = productDto.ProductCategoryId;

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}