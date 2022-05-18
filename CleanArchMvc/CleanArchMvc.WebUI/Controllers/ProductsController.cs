using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
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
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }
        #endregion
    }
}
