using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CleanArchMvc.IoC
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfraStructureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            //Informa o tipo de autenticação JWT-Bearer
            //Definir o modelo de desafio de autenticação
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //Habilita a autenticação JWT usando o esquema de desafio definidos
            //Valida o Token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    //Valores Válidos
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),

                    //Valor padrão do ciclo de vida é de 5 minutos, ou seja, o tempo do Token que foi definido + 5.
                    //Quando Zera essa propriedade, estaremos passando o tempo expiração definidas por nós
                    ClockSkew =  TimeSpan.Zero 
                };
            });

            return services;
        }
    }
}
