using Microsoft.EntityFrameworkCore;
using SplitTeam.Model;
using SplitTeam.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
        Task<User> GetUserById(int userId);
        Task<List<User>> GetAll();
        Task<User> GetUserFromUsernameAndPassword(string username, string password);
        Task<bool> UserExists(string username);

    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.Include(u=>u.Role).ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserFromUsernameAndPassword(string username, string password)
        {
            return await _context.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;
            return false;
        }
    }
}
