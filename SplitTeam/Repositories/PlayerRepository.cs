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
        Task<Player> GetPlayerById(int playerId);
        Task<bool> DeletePlayerWithRanks(int playerId);
        Task<List<Player>> GetAllPlayers();
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _context;
        private readonly IPlayerRankRepository _playerRankRepository;

        public PlayerRepository(DataContext context, IPlayerRankRepository playerRankRepository)
        {
            _playerRankRepository = playerRankRepository;
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

        public async Task<bool> DeletePlayerWithRanks(int playerId)
        {
            var ranksForPlayer = await _context.PlayerRanks.Where(pr => pr.Player.Id == playerId).ToListAsync();
            foreach (var rank in ranksForPlayer)
            {
                await _playerRankRepository.DeletePlayerRank(rank.Id);
            }
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
            var playerDeleted = _context.Remove(player);
            await _context.SaveChangesAsync();
            return playerDeleted == null ? false : true;
        }

        public async Task<List<Player>> GetAllPlayers() { 
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> GetPlayerById(int playerId)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        }
    }
}
