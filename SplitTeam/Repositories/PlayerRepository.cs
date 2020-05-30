using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SplitTeam.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> AddPlayer(Player player);
        Task<bool> DeletePlayer(int playerId);
        Task<List<Player>> GetAllPlayers();
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Player> AddPlayer(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<bool> DeletePlayer(int playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
            var playerDeleted = _context.Remove(player);
            _context.SaveChanges();
            return playerDeleted == null ? false : true;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _context.Players.ToListAsync();
        }
    }
}
