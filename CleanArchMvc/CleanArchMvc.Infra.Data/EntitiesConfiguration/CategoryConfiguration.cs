using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); // PK

            //Recurso da FluentAPI
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            //Propriedade HasData da FluentAPI: Popula a tabela Categoria quando aplicara a migration
            builder.HasData(
                new Category(1, "Cabo de Vela"),    
                new Category(2, "Velas de Iridium"),    
                new Category(3, "Filtro de Ar Esportivo InBox")
            );
        }
    }
}
