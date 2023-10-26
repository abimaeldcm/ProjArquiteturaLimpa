using CleanArch.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IUserMktService
    {
        Task<UserMkt> GetEmployeeByIdAsync(int? id);
    }
}
