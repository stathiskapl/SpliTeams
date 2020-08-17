using Microsoft.EntityFrameworkCore;
using SplitTeam.Model;
using SplitTeam.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Repositories
{
    public interface IStatsRepository
    {
        Task<List<MatchWithTeamAndPlayersDto>> getMatchesWithTeamsAndPlayers();
    }
    public class StatsRepository : IStatsRepository
    {
        private readonly DataContext _context;
        public StatsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<MatchWithTeamAndPlayersDto>> getMatchesWithTeamsAndPlayers()
        {
            var matchWithTeamAndPlayersDto = new List<MatchWithTeamAndPlayersDto>();
            var matches = await _context.Matches.Include(m=>m.TeamA).Include(m=>m.TeamB).ToListAsync();
            foreach (var match in matches) {
                matchWithTeamAndPlayersDto.Add(new MatchWithTeamAndPlayersDto
                {
                    Description = match.Description,
                    ScoreTeamA = match.ScoreTeamA,
                    ScoreTeamB = match.ScoreTeamB,
                    TeamA = match.TeamA,
                    TeamB = match.TeamB,
                    TeamPlayerA = await _context.TeamPlayers.Where(tp => tp.Team == match.TeamA).Include(tp => tp.Player).ToListAsync(),
                    TeamPlayerB = await _context.TeamPlayers.Where(tp => tp.Team == match.TeamB).Include(tp => tp.Player).ToListAsync(),

                });

            }
            return matchWithTeamAndPlayersDto;
        }
    }
}
