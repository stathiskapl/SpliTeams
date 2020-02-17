using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface ISkillService
    {
        Task<Skill> AddSkill(Skill skill);
        Task<List<Skill>> GetAllSkills();
    }

    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<Skill> AddSkill(Skill skill)
        {
            return await _repository.AddSkill(skill);
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            return await _repository.GetAllSkills();
        }
    }
}
