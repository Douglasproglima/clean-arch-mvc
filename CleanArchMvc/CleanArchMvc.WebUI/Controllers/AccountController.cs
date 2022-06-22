using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        #region Atributos/Propriedades
        private readonly IAuthenticate _authentication;
        #endregion

        #region Construtor
        public AccountController(IAuthenticate authentication)
        {
            _authentication = authentication;
        }
        #endregion

        #region Métodos Cadastrar Login
        #endregion

        #region Métodos Login
        [HttpGet]
        public IActionResult Login (string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginDTO)
        {
            var result = await _authentication.Authenticate(loginDTO.Email, loginDTO.Password);

            if (result)
            {
                if(string.IsNullOrEmpty(loginDTO.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(loginDTO.ReturnUrl);
            }
            else
            { 
                ModelState.AddModelError(string.Empty, "Falha ao realizar login. A senha precisa ser mais segura.");
                return View(loginDTO);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerDTO)
        {
            var result = await _authentication.RegisterUser(registerDTO.Email, registerDTO.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Falha ao cadastrar usuário. A senha precisa ser mais segura.");
                return View(registerDTO);
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Login");
        }
        #endregion
    }
}
