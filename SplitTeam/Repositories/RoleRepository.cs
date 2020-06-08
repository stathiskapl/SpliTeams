using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SplitTeam.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> AddRole(Role role);
        Task<Role> GetDefaultRole();
        int GetDefaultRoleId();
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Role> AddRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();

        }

        public async Task<Role> GetDefaultRole()
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Plain User");
        }

        public int GetDefaultRoleId()
        {
            return _context.Roles.FirstOrDefault(r=>r.Id == 1).Id;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
