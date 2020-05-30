using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface IPlayerRankService
    {
        Task<PlayerRank> AddPlayerRank(PlayerRankCreateDTO playerRankCreateDto);
        Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId);
        Task<List<PlayerRank>> GetAll();
    }

    public class PlayerRankService : IPlayerRankService
    {
        private readonly IPlayerRankRepository _repository;

        public PlayerRankService(IPlayerRankRepository repository)
        {
            _repository = repository;
        }
        public async Task<PlayerRank> AddPlayerRank(PlayerRankCreateDTO playerRankCreateDto)
        {
            var allPlayerRanksForPlayer = await GetAllPlayerRanksForPlayer(playerRankCreateDto.PlayerId);
            var counter = 0;
            foreach (var playerRanksForPlayer in allPlayerRanksForPlayer) {
                if (playerRankCreateDto.SkillId == playerRanksForPlayer.Skill.Id)
                {
                    counter++;
                }
            }
            if (counter == 0)
            {
                return await _repository.AddNewPlayerRank(playerRankCreateDto);
            }
            return null;
        }

        public async Task<List<PlayerRank>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId)
        {
            return await _repository.GetAllPlayerRanksForPlayer(playerId);
        }
    }
}
