using CleanArchMvc.Domain.Interfaces.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        #region Métodos Cadastrar Login
        #endregion

        #region Métodos Login
        [HttpGet]
        public IActionResult Login (string url)
        {
            return View(new LoginViewDTO()
            {
                Url = url
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewDTO loginDTO)
        {
            var result = await _authenticate.Authenticate(loginDTO.Email, loginDTO.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Falha ao realizar login. A senha precisa ser mais segura.");
                return View(loginDTO);
            }

            if(string.IsNullOrEmpty(loginDTO.Url))
                return RedirectToAction("Index", "Home");

            return RedirectToAction(loginDTO.Url);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewDTO registerDTO)
        {
            var result = await _authenticate.RegisterUser(registerDTO.Email, registerDTO.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Falha ao cadastrar usuário. A senha precisa ser mais segura.");
                return View(registerDTO);
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return Redirect("/Account/Login");
        }
        #endregion
    }
}
