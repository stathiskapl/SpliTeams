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
        Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId);
        Task<List<PlayerRank>> GetAll();


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
    }
}
