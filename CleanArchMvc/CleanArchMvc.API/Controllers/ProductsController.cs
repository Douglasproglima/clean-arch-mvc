using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        #region Propriedades/Atributos/Variáveis
        private readonly IProductService _productService;

        #endregion

        #region Construtor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Métodos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products is null)
                return NotFound("Produtos não encontradas.");

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if (product is null)
                return NotFound("Produto não encontrada.");

            return Ok(product);
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Dados Invádlidos!");

            await _productService.Add(productDTO);

            var newProduct = new CreatedAtRouteResult(
                "GetProduct",
                new
                {
                    id = productDTO.Id
                },
                productDTO
            );

            return newProduct;
        }

        [HttpPut("{id:int}", Name = "PutProduct")]
        public async Task<ActionResult> Put(int? id, [FromBody] ProductDTO productDTO)
        {
            if (id is null) return NotFound("Campo Id é obrigatório.");
            if (productDTO is null) return BadRequest();
            if (id != productDTO.Id) return BadRequest($"Produto não encontrada para o Id: {id}");

            await _productService.Update(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}"), ActionName("DeleteProduct")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product is null)
                return NotFound($"Produto não encontrada para o Id: {id}");

            await _productService.Remove(id);

            return Ok(product);
        }
        #endregion
    }
}
