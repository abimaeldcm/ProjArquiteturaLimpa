using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return _context.Products.SingleOrDefault(x => x.Id == id);             
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges(); ;
        }
        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
