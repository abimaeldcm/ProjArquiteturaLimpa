using CleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Product GetById(int? id);
        void Add(Product product); 
        void Update(Product product);
        void Delete(Product product);

    }
}
