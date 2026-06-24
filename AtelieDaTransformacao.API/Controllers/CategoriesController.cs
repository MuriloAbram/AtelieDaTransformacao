using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        /// <summary>
        /// Encapsulamento
        /// </summary>
        private readonly IProductCategoryService _productCategoryService;

        public CategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Volta todas as cayegorias 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll()
        {
            var categories = await _productCategoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Adm cria categoria
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductCategoryDto>> Create([FromBody] CreateProductCategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Os dados da categoria não podem ser nulos.");
            }

            // 1. Mapeia manualmente o CreateProductCategoryDto para o ProductCategoryDto esperado pelo serviço
            var categoryDto = new ProductCategoryDto
            {
                Name = dto.Name
                // Se o seu ProductCategoryDto tiver outras propriedades, preencha-as aqui
            };

            // 2. Executa o método de salvar (sem atribuição de variável, já que ele retorna void/Task)
            await _productCategoryService.AddAsync(categoryDto);

            // 3. Retorna o status 201 Created passando o próprio objeto que criamos
            return CreatedAtAction(nameof(GetAll), new { id = categoryDto.Id }, categoryDto);
        }
    }
}
