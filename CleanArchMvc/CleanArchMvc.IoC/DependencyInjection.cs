using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappgins;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Interfaces.Account;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchMvc.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            /*
             * 1 - Registrar o contexto da aplicação
             * 2 - Definir o provedor do DB 
             * 3 - Definir a string de conexão (WebUI -> appsettings.json)
             * 4 - Informar o local onde ficará a migração(No caso o projeto 
             *     onde está definido o arquivo de contexto INFRA.DATA)
             */
            services.AddDbContext<ApplicationDbContext>(
                //Montar conexão string https://www.connectionstrings.com/
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), //WebUI -> appsettings.json
                    assembly => assembly.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
                )
            );

            RegisterAppCookie(services);
            RegisterServicesIdentity(services);
            RegisterScopedEntities(services);

            //Retorna o assembly a onde foi definido os Handlers do CQRS
            var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(myHandlers);

            return services;
        }

        internal static void RegisterAppCookie(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");
        }

        internal static void RegisterServicesIdentity(IServiceCollection services)
        {
            //Serviços do Identity
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Registrar o Serviços implementados na camada Domain (Autenticação, User e Role)
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        }

        internal static void RegisterScopedEntities(IServiceCollection services)
        {
            /* Transient(AddTransient): Cria os objs a cada vez que forem solicitados;
             * Scoped(AddScoped)......: Cria os objs uma vez por solicitação;
             * Singleton(AddSingleton): Cria os objs apenas na primeira vez que for solicitado.
             */

            //Recomendação para App Web é AddScoped.
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        }
    }
}
