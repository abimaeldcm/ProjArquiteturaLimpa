using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2);

            //VAI VERIFCAR SE A TABELA PRODUCT POSSUI DADOS. SE NÃO, ELE VAI CRIAR COM ESSAS INFORMAÇÕES.
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Creme",
                    Description = "Creme para cabelos cacheados",
                    Price = 18.50M
                },
                new Product
                {
                    Id = 2,
                    Name = "Shampoo",
                    Description = "Shampoo para cabelos cacheados",
                    Price = 20.45M
                }
                );

        }
    }
}

