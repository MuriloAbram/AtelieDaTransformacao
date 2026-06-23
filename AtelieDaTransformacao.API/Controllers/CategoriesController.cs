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
            var category = await _productCategoryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
        }
    }
}
