using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<ProductViewModel> GetById(int? id);
        void Add(ProductViewModel product);
        void Update(ProductViewModel product);
        void Delete(int id);
    }
}
