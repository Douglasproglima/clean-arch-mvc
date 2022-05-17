using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            /*
             * 1 - Registrar o contexto da aplicação
             * 2 - Definir o provedor do DB 
             * 3 - Definir a string de conexão
             * 4 - Informar o local onde ficará a migração(No caso o projeto 
             *     onde está definido o arquivo de contexto INFRA.DATA)
             */
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
                )
            );

            /* Transient(AddTransient): Cria os objs a cada vez que forem solicitados;
             * Scoped(AddScoped)......: Cria os objs uma vez por solicitação;
             * Singleton(AddSingleton): Cria os objs apenas na primeira vez que for solicitado.
             */

            //Recomendação para App Web é AddScoped.
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
