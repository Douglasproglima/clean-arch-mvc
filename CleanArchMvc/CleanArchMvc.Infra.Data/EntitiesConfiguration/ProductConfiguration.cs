﻿using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Price).HasPrecision(10, 2);
            
            //HasOne e WithMany: Config do relacionamento UM para MUITOS
            //UMA Categoria pode ter MUITOS produtos
            builder.HasOne(entity => entity.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }
}
