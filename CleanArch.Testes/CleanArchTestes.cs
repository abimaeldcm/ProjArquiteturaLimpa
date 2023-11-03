using AutoMapper;
using CleanArch.Application.Authentication;
using CleanArch.Application.Services;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using System;
using Xunit;

namespace CleanArch.Testes
{
    public class CleanArchTestes
    {
        [Fact]
        public void CreateTokenTest()
        {
            string token = TokenService.GenerateToken(new UserMkt
            {
                Id = 1,
                Username = "Abimael",
                Password = "teste",
                Role = "manager"
            });

            Assert.IsType<string>(token);
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        public class ProductTest
        {
            [Fact]
            public void GetProducts_ShouldReturnListOfProducts()
            {
                // Arrange
                var mockRepository = new Mock<IProductRepository>();
                var mockMapper = new Mock<IMapper>();

                // Configurar o mock para retornar um Product quando GetById é chamado com 1
                mockRepository.Setup(repo => repo.GetById(1))
                             .Returns(new Product { Id = 1, Name = "Product 1" });

                // Configurar o mock do Mapper para retornar um ProductViewModel quando Map é chamado
                mockMapper.Setup(mapper => mapper.Map<ProductViewModel>(It.IsAny<Product>()))
                          .Returns(new ProductViewModel { Id = 1, Name = "Product 1" });

                var productService = new ProductService(mockRepository.Object, mockMapper.Object);

                // Act
                var result = productService.GetById(1);

                // Assert
                Assert.IsType<ProductViewModel>(result);
            }
        }
        public class ProductRepositoryTests
        {
            [Fact]
            public void GetById_ValidId_ReturnsProduct()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    var product = new Product 
                    { Id = 1, Name = "ProductTest1",Description = "TestingProduct", Price = 12.00M };
                    context.Products.Add(product);
                    context.SaveChanges();
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new ProductRepository(context);

                    // Act
                    var result = repository.GetById(1);

                    // Assert
                    Assert.NotNull(result);
                    Assert.Equal(1, result.Id);
                    Assert.Equal("ProductTest1", result.Name);
                }
            }

            [Fact]
            public async Task GetProducts_NoProducts_ThrowsException()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    context.Products.RemoveRange(context.Products);
                    context.SaveChanges();
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new ProductRepository(context);

                    // Act and Assert
                    await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.GetProducts());
                }
            }
        }
    }
}
