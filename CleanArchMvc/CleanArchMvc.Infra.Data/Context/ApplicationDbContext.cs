using CleanArchMvc.Domain;
using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        /* Uma instância da classe DbContext representa uma sessão no banco de dados.
         * Que pode ser usado para interragir com o DB(TSQL)
         * DbContext é uma combinação dos padrões: UnitOfWork e Repository
         */

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Permite configurar o modelo usando a FluenteApi e uma instância de ModelBuilder
            base.OnModelCreating(builder);

            //Aplica algumas configurações nas entidades Product e Category definida no assembly ApplicationDbContext
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
