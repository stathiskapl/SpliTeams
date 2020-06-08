using Newtonsoft.Json;
using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Services;

namespace SplitTeam.Helpers
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedRoles()
        {
            var rolesInDbCount = _dataContext.Roles.Count();
            if (rolesInDbCount == 0)
            {
                var rolesData = System.IO.File.ReadAllText("Helpers/RolesSeedData.json");
                var roles = JsonConvert.DeserializeObject<List<Role>>(rolesData);
                foreach (var role in roles)
                {
                    _dataContext.Add(role);
                }
                _dataContext.SaveChanges();
            }
        }
    }
}
