using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Model;

namespace SplitTeam.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> AddTeam(Team team);
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;
        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Team> AddTeam(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }
    }
}
