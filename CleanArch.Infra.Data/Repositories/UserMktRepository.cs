using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repositories
{
    public class UserMktRepository : IUserMktRepository
    {
        protected private ApplicationDbContext _context;

        public UserMktRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserMkt>> GetUsers()
        {
            return await _context.UserMkts.ToListAsync();
        }
        public async Task<UserMkt> GetById(int? id)
        {
            return await _context.UserMkts.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UserMkt> Add(UserMkt user)
        {
            var entity = await _context.UserMkts.AddAsync(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<UserMkt> Update(UserMkt user)
        {
            var userDb = _context.UserMkts.Update(user);
            await _context.SaveChangesAsync();

            return userDb.Entity;
        }
        public async void Delete(UserMkt user)
        {
            _context.UserMkts.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
