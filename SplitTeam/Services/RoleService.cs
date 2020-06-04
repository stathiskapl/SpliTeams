using SplitTeam.Model;
using SplitTeam.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Services
{
    public interface IRoleService
    {
        Task<Role> AddRole(Role role);
        Task<List<Role>> GetAllRoles();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }
        public async Task<Role> AddRole(Role role)
        {
            return await _repository.AddRole(role);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _repository.GetAllRoles();
        }
    }
}
