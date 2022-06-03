using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /* Uma instância da classe DbContext representa uma sessão no banco de dados.
         * Que pode ser usado para interragir com o DB(TSQL)
         * DbContext é uma combinação dos padrões: UnitOfWork e Repository
         */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Permite configurar o modelo usando a FluenteApi e uma instância de ModelBuilder
            base.OnModelCreating(builder);

            //Aplica algumas configurações nas entidades Product e Category definida no assembly ApplicationDbContext
            //Vai procurar pelo arquivo de contexto, vai verificar as entidaes do DbSet<T>
            //Como as classes da pasta EntitiesConfiguration herdam de IEntityTypeConfiguration ele vai enteder que a config foi definida
            //Sendo irá fazer o reflexion e dar seguimento
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //Se não configurar em arquivos(classes) separadas a implementação seria a seguir:
            /*
            builder.ApplyConfiguration(new EntitiesConfiguration.CategoryConfiguration());
            builder.ApplyConfiguration(new EntitiesConfiguration.ProductConfiguration());
            */
        }
    }
}
