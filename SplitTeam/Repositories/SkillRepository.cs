using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SplitTeam.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill> AddSkill(Skill skill);
        Task<List<Skill>> GetAllSkills();
    }

    public class SkillRepository : ISkillRepository
    {
        private readonly DataContext _context;

        public SkillRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Skill> AddSkill(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            return await _context.Skills.ToListAsync();
        }
    }
}
