using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SplitTeam.Model;
using SplitTeam.ModelDtos;

namespace SplitTeam.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> AddTeam(Team team);
        Task<Team> GetTeamById(int teamId);
        Task<List<TeamPlayer>> SaveTeamPlayers(List<TeamPlayer> teamsPlayers);
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

        public async Task<Team> GetTeamById(int teamId)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<List<TeamPlayer>> SaveTeamPlayers(List<TeamPlayer> teamsPlayers)
        {
            foreach (var teamsPlayer in teamsPlayers)
            {
                _context.TeamPlayers.Add(teamsPlayer);
            }
            await _context.SaveChangesAsync();
            return teamsPlayers;
        }
    }
}
