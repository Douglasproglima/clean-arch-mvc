using CleanArchMvc.API.DTOS;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Atributos/Propriedades
        private readonly IAuthenticate _authenticate;
        #endregion
        
        #region Construtor
        public TokenController(IAuthenticate authenticate)
        {
            _authenticate = authenticate ?? 
                throw new ArgumentNullException(nameof(authenticate));
        }
        #endregion

        #region Métodos Login
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result)
            { 
                return Ok($"User: { userInfo } - Login realizado com sucesso.");
            //return GenarateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Falha ao realizar login!");
                return BadRequest(ModelState);
            }
        }
        #endregion
    }
}
