using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SplitTeam.Repositories
{
    public interface IMatchRepository
    {
        Task<Match> CreateMatch(Match match);
        Task<List<Match>> GetAllMatches();
    }

    public class MatchRepository : IMatchRepository
    {
        private readonly DataContext _context;

        public MatchRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Match> CreateMatch(Match match)
        {
            await _context.AddAsync(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<List<Match>> GetAllMatches()
        {
            return await _context.Matches
                .Include(m=>m.TeamA)
                .Include(m => m.TeamB)
                .ToListAsync();
        }
    }
}
