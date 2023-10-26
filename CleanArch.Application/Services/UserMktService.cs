using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class UserMktService : IUserMktService
    {
        private readonly IUserMktRepository _userRepository;

        public UserMktService(IUserMktRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserMkt> GetEmployeeByIdAsync(int? id)
        {
           return await _userRepository.GetById(id);
        }
    }
}
