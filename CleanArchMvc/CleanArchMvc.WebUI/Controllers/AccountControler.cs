using CleanArchMvc.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountControler : Controller
    {
        #region Atributos/Propriedades
        private readonly IAuthenticate _authenticate;
        #endregion

        #region Construtor
        public AccountControler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }
        #endregion

        #region
        #endregion
        public IActionResult Index()
        {
            return View();
        }
    }
}
