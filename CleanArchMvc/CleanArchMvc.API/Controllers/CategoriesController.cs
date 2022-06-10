using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region Propriedades/Atributos/Variáveis
        private readonly ICategoryService _categoryService;

        #endregion

        #region Construtor
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if (categories is null)
                return NotFound("Categorias não encontradas.");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int? id)
        {
            var category = await _categoryService.GetById(id);
            if (category is null)
                return NotFound("Categoria não encontrada.");

            return Ok(category);
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Dados Invádlidos!");

            await _categoryService.Add(categoryDTO);

            var newCategory = new CreatedAtRouteResult(
                "GetCategory",
                new
                {
                    id = categoryDTO.Id
                },
                categoryDTO
            );

            return newCategory;
        }

        [HttpPut("{id:int}", Name = "PutCategory")]
        public async Task<ActionResult> Put(int? id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id is null) return NotFound("Campo Id é obrigatório.");
            if (categoryDTO is null) return BadRequest();
            if (id != categoryDTO.Id) return BadRequest($"Categoria não encontrada para o Id: {id}");

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}"), ActionName("DeleteCategory")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.GetById(id);

            if(category is null)
                return NotFound($"Categoria não encontrada para o Id: {id}");

            await _categoryService.Remove(id);

            return Ok(category);
        }
        #endregion
    }
}
