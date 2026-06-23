using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retorna todas os jogos cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var product = await _productService.GetAllAsync();
            return Ok(product);
        }

        /// <summary>
        /// Retorna produto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

    }
}
