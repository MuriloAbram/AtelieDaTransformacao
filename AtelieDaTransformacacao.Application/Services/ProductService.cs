using System;
using System.Collections.Generic;
using System.Linq;
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
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        // Repositório já retorna um único Product? — não é necessário chamar FirstOrDefault()
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
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    // IMPLEMENTAÇÃO OBRIGATÓRIA DA INTERFACE
    public async Task<IEnumerable<ProductDto>> GetFeaturedAsync()
    {
        // Caso o repositório não tenha GetFeaturedAsync, você pode buscar todos e filtrar, ou ajustar seu repositório
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
                WhatsAppLink = _whatsAppService.GenerateProductInquiryLink(item.Title, item.Price)
            });
        }
        return dtos;
    }

    // IMPLEMENTAÇÃO OBRIGATÓRIA DA INTERFACE
    public async Task<int> CountAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Count();
    }

    public async Task AddAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Title = productDto.Title,
            Description = productDto.Description,
            Price = productDto.Price,
            Image = productDto.Image,
            CategoryId = productDto.CategoryId
        };
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        // Repositório já retorna um único Product? — não é necessário chamar FirstOrDefault()
        var product = await _productRepository.GetByIdAsync(productDto.Id);
        if (product == null) throw new Exception("Product not found");

        // Copia os valores do DTO para a entidade antes de persistir
        product.Title = productDto.Title;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.Image = productDto.Image;
        product.CategoryId = productDto.CategoryId;

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

}