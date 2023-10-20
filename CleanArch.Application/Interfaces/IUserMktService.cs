using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IUserMktService
    {
        Task<IEnumerable<UserMkt>> GetUsersMkt();
        Task<UserMkt> GetById(int? id);
        Task<UserMkt> Add(UserMkt user);
        Task<UserMkt> Update(UserMkt user);
        void Delete(int id);
    }
}
