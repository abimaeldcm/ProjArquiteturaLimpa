using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infra.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserMkt>
    {
        public void Configure(EntityTypeBuilder<UserMkt> builder)
        {
            builder.Property(p => p.Username).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Role).HasMaxLength(100).IsRequired();

            
            builder.HasData(
                new UserMkt
                {
                    Id = 1,
                    Username = "Teste",
                    Password = "teste",
                    Role = "manager"
                });

        }
    }
}

