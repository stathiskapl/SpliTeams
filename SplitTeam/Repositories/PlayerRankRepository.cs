using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SplitTeam.Model;

namespace SplitTeam.Repositories
{
    public interface IPlayerRankRepository
    {
        Task<PlayerRank> AddNewPlayerRank(PlayerRankCreateDTO playerRankCreateDto);
        Task<PlayerRank> SavePlayerRank(PlayerRank playerRank);
        Task<bool> DeletePlayerRank(int playerRankId);
        Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId);
        Task<List<PlayerRank>> GetAllPlayerRanksForUser(int userId);
        Task<List<PlayerRank>> GetAll();
        Task<PlayerRank> UpdatePlayerRank(int rankId,PlayerRankCreateDTO playerRankCreateDTO);
        Task<PlayerRank> UpdatePlayerRankToSave(PlayerRankSaveDTO playerRankCreateDTO);
        Task<bool> SaveChangesAsync();

    }

    public class PlayerRankRepository : IPlayerRankRepository
    {
        private readonly DataContext _context;

        public PlayerRankRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PlayerRank> AddNewPlayerRank(PlayerRankCreateDTO playerRankCreateDto)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == playerRankCreateDto.PlayerId);
            var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Id == playerRankCreateDto.SkillId);
            PlayerRank playerRank = new PlayerRank()
            {
                Player = player,
                Skill = skill,
                Rank = playerRankCreateDto.Rank
            };

            await _context.PlayerRanks.AddAsync(playerRank);
            await _context.SaveChangesAsync();
            return playerRank;
        }

        public async Task<bool> DeletePlayerRank(int playerRankId)
        {
            var playerRank = await _context.PlayerRanks.FirstOrDefaultAsync(pr => pr.Id == playerRankId);
            var playerRankDeleted = _context.Remove(playerRank);
            return playerRankDeleted == null ? false : true;

        }

        public async Task<List<PlayerRank>> GetAll()
        {
            return await _context.PlayerRanks
                .Include(x => x.Skill)
                .Include(x => x.Player)
                .ToListAsync();
        }

        public async Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId)
        {
            return await _context.PlayerRanks.Where(x => x.Player.Id == playerId)
                .Include(x=>x.Skill)
                .Include(x=>x.Player)
                .ToListAsync();
        }

        public async Task<List<PlayerRank>> GetAllPlayerRanksForUser(int userId)
        {
            return await _context.PlayerRanks.Where(x => x.User.Id == userId)
                .Include(x => x.Skill)
                .Include(x => x.Player)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PlayerRank> SavePlayerRank(PlayerRank playerRank)
        {
            var a = await _context.PlayerRanks.AddAsync(playerRank);
            return a.Entity;
        }

        public async Task<PlayerRank> UpdatePlayerRank(int rankId,PlayerRankCreateDTO playerRankCreateDTO)
        {
            var playerRank = await _context.PlayerRanks.FirstOrDefaultAsync(pr=>pr.Id == rankId);
            playerRank.Rank = playerRankCreateDTO.Rank;
            var playerRankUpdated = _context.Update(playerRank);
            await _context.SaveChangesAsync();
            return playerRankUpdated.Entity;
        }

        public async Task<PlayerRank> UpdatePlayerRankToSave(PlayerRankSaveDTO playerRankCreateDTO)
        {
            var playerRank = await _context.PlayerRanks.FirstOrDefaultAsync(pr => pr.Id == playerRankCreateDTO.Id);
            playerRank.Rank = playerRankCreateDTO.Rank;
            var playerRankUpdated = _context.Update(playerRank);
            return playerRankUpdated.Entity;
        }
    }
}
