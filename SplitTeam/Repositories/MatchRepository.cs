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
        Task<Match> UpdateMatch(Match match);
        Task<Match> GetMatchById(int matchId);
        

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

        public async Task<Match> GetMatchById(int matchId)
        {
            return await _context.Matches.Include(m=>m.TeamA).Include(m=>m.TeamB).FirstOrDefaultAsync(m => m.Id == matchId);
        }

        public async Task<Match> UpdateMatch(Match match)
        {
            var matchToUpdate = await _context.Matches.FirstOrDefaultAsync(m => m.Id == match.Id);
            matchToUpdate.ScoreTeamA = match.ScoreTeamA;
            matchToUpdate.ScoreTeamB = match.ScoreTeamB;
            matchToUpdate.Description = match.Description;
            _context.Update(matchToUpdate);
            await _context.SaveChangesAsync();
            return match;
        }
    }
}
