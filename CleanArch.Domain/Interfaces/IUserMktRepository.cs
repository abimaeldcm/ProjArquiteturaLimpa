using CleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface IUserMktRepository
    {
        Task<IEnumerable<UserMkt>> GetUsers();
        Task<UserMkt> GetById(int? id);
        Task<UserMkt> Add(UserMkt user);
        Task<UserMkt> Update(UserMkt user);
        void Delete(UserMkt user);


    }
}
