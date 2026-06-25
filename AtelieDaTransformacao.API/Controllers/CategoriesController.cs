using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Volta todas as categorias
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll()
        {
            var categories = await _productCategoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Adm cria categoria
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> Create([FromBody] CreateProductCategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Os dados da categoria não podem ser nulos.");
            }

            var categoryDto = new ProductCategoryDto
            {
                Name = dto.Name
            };

            await _productCategoryService.AddAsync(categoryDto);

            return CreatedAtAction(nameof(GetAll), new { id = categoryDto.Id }, categoryDto);
        }

        /// <summary>
        /// Deleta uma categoria indesejada pelo ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // 1. Busca se a categoria existe antes de tentar deletar
            var category = await _productCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"A categoria com o ID {id} não foi encontrada." });
            }

            try
            {
                // 2. Executa a exclusão na camada de serviço
                await _productCategoryService.DeleteAsync(id);
                return Ok(new { message = "Categoria removida com sucesso do Ateliê!" });
            }
            catch (System.Exception ex)
            {
                // 3. Evita que o app quebre se houver restrição de chave estrangeira no banco de dados
                return BadRequest(new
                {
                    message = "Não foi possível deletar a categoria. Verifique se não existem produtos vinculados a ela.",
                    details = ex.Message
                });
            }
        }
    }
}