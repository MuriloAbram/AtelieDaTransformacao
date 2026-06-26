using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(new { message = "Produto não encontrado" });
            }

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Os dados do produto não podem ser nulos.");
            }

            var productDto = new ProductDto
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Image = dto.Image,
                CategoryId = dto.CategoryId,
                IsFeatured = true, // Define como destaque por padrão
                IsAvailable = true
            };

            // Certifique-se de que o seu AddAsync modifique o productDto inserindo o ID gerado pelo banco,
            // ou altere a assinatura do seu serviço para retornar o DTO persistido e populado:
            // productDto = await _productService.AddAsync(productDto);
            await _productService.AddAsync(productDto);

            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }
    }
}