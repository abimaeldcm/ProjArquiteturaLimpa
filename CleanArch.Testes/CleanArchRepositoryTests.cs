using CleanArch.Domain.Entities;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace CleanArch.Testes
{
    public class CleanArchRepositoryTests
    {
        [Fact]
        public async void GetProducts_ReturnListOfProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProducts_ReturnListOfProducts")
                .Options;

            using (var contextDB = new ApplicationDbContext(options))
            {
                var product = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Product 1",
                        Description = "new product in local db",
                        Price = 10.50M
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Product 2",
                        Description = "another product",
                        Price = 20.00M
                    }
                };
                contextDB.Products.AddRange(product);
                contextDB.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var ProdctRep = new ProductRepository(context);

                // Act
                var result = await ProdctRep.GetProducts();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<List<Product>>(result);
            };

        }
        [Fact]
        public async void GetProducts_ReturnDatabaseIsEmpty()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProducts_ReturnDatabaseIsEmpty")
                .Options;
                 
            using (var context = new ApplicationDbContext(options))
            {
                var ProdctRep = new ProductRepository(context);

                await Assert.ThrowsAsync<DbUpdateException>(() => ProdctRep.GetProducts());
            };
        }
        [Fact]
        public void GetById_ValidId_ReturnsProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetById_ValidId_ReturnsProduct")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var product = new Product
                { Id = 1, Name = "ProductTest1", Description = "TestingProduct", Price = 12.00M };
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
                .UseInMemoryDatabase(databaseName: "GetProducts_NoProducts_ThrowsException")
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
