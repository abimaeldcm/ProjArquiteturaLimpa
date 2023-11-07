using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Product GetById(int? id)
        {
            try
            {
                var Product = _context.Products.SingleOrDefault(x => x.Id == id);

                return Product == default ?
                    throw new Exception($"Entity with ID {id} was not found.")
                    : Product;
            }
            catch (Exception erro)
            {

                throw new ObjectNotFoundException("An error occurred while searching for the desired products." + erro.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return products.Any() ?
                    products
                    : throw new Exception($"The Database is empty.");
            }
            catch (Exception erro)
            {
                throw new DbUpdateException(
                    "An error occurred when searching for the desired products. " + erro.Message);
            }
        }
        public Product Add(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception erro)
            {
                throw new DbUpdateException(
                    "An error occurred while adding the desired product. " + erro.Message);
            }
        }
        public void Update(Product product)
        {
            try
            {
                _context.Update(product);
                _context.SaveChanges();
            }
            catch (Exception erro)
            {

                throw new DbUpdateException(
                    "An error occurred while updating the desired product. " + erro.Message);
            }
        }
        public void Delete(Product product)
        {
            try
            {
                _context.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception erro)
            {
                throw new DbUpdateException(
                    "An error occurred while deleting the desired product. " + erro.Message);
            }
        }
    }
}