using CleanArch.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface IUserMktRepository
    {
        Task<UserMkt> GetById(int? id);
    }
}
