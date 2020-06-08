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
                var rolesData = System.IO.File.ReadAllText("Helpers/SeedJsonData/RolesSeedData.json");
                var roles = JsonConvert.DeserializeObject<List<Role>>(rolesData);
                foreach (var role in roles)
                {
                    _dataContext.Add(role);
                }
                _dataContext.SaveChanges();
                CreateAdminUser();
            }
        }

        public void CreateAdminUser()
        {
            var adminUser = new User();
            var user = _dataContext.Users.FirstOrDefault(u => u.UserName == "admin");
            if (user == null)
            {
                var adminRole = _dataContext.Roles.FirstOrDefault(r => r.Name == "Admin");
                adminUser.UserName = "admin";
                adminUser.Password = "admin";
                adminUser.Role = adminRole;
                _dataContext.Users.Add(adminUser);
                _dataContext.SaveChanges();
            }
        }

        public void SeedSkills()
        {
            var skills = _dataContext.Skills.Count();
            if (skills == 0) 
            {
                var skillsData = System.IO.File.ReadAllText("Helpers/SeedJsonData/SkillsSeedData.json");
                var skillsToSave = JsonConvert.DeserializeObject<List<Skill>>(skillsData);
                foreach (var skillToSave in skillsToSave)
                {
                    _dataContext.Add(skillToSave);
                }
                _dataContext.SaveChanges();
            }
        }

    }
}
