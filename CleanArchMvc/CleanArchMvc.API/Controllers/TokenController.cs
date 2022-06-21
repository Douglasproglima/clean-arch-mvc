using CleanArchMvc.API.DTOS;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Atributos/Propriedades
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        #endregion
        
        #region Construtor
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? 
                throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }
        #endregion

        #region Métodos Login
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
            if (result)
                return GenarateToken(userInfo);
            else
            {
                ModelState.AddModelError(string.Empty, "Falha ao realizar login!");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenarateToken(LoginDTO userInfo)
        {
            //Declaração do usuário
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Geração da chave primária para assinar o Token
            var jwtConfigurationSecretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var privateKey = new SymmetricSecurityKey(jwtConfigurationSecretKey);

            //Gerar assinatura digital do Token
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //Definição do tempo de expiração do Token
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //Geração do Token
            JwtSecurityToken token = new JwtSecurityToken(
                //Emissor
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            var userToken = new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

            return userToken;
        }
        #endregion
    }
}
