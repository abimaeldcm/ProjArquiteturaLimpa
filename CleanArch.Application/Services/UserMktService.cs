using AutoMapper;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class UserMktService : IUserMktService
    {
        protected readonly IUserMktRepository _userMktRepository;

        public UserMktService(IUserMktRepository userMktRepository)
        {
            _userMktRepository = userMktRepository;
        }
        public async Task<IEnumerable<UserMkt>> GetUsersMkt()
        {
            return await _userMktRepository.GetUsers();
        }
        public async Task<UserMkt> GetById(int? id)
        {
            return await _userMktRepository.GetById(id);
        }
        public async Task<UserMkt> Add(UserMkt user)
        {
            return await _userMktRepository.Add(user);             
        }
        public async Task<UserMkt> Update(UserMkt user)
        {
            return await _userMktRepository.Update(user);
        }
        public async void Delete(int id)
        {
            var user = await GetById(id);
            _userMktRepository.Delete(user);
        }
    }
}
