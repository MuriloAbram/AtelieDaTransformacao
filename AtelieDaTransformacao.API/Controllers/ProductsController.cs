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
    public class ProductsController : ControllerBase // Alterado para ControllerBase (ideal para APIs puras)
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retorna um produto por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(new { message = "Produto não encontrado" });
            }

            return Ok(product);
        }

        /// <summary>
        /// Cadastra um novo produto (Apenas Administradores).
        /// </summary>
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
                IsFeatured = dto.IsFeatured,
                IsAvailable = true
            };

            // Apenas aguarda a execução do método, sem tentar atribuir o retorno à uma variável
            await _productService.AddAsync(productDto);

            // Retornamos o próprio 'productDto' preenchido. 
            // Nota: Se o seu banco gera o Id automaticamente, o Id aqui irá como 0. 
            // Se o Id for crucial no retorno da API, prefira a Opção 2 abaixo.
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }
    }
}