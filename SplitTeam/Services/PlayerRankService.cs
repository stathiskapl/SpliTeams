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
        Task<PlayerRank> UpdatePlayerRank(int rankId,PlayerRankCreateDTO playerRankCreateDTO);
        Task<List<PlayerRankCreateDTO>> AddPlayerRankWhenPlayerCreated(int playerId);
        Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId);
        Task<List<PlayerRank>> GetAll();
    }

    public class PlayerRankService : IPlayerRankService
    {
        private readonly IPlayerRankRepository _repository;
        private readonly ISkillRepository _skillRepository;

        public PlayerRankService(IPlayerRankRepository repository,ISkillRepository skillRepository)
        {
            _repository = repository;
            _skillRepository = skillRepository;
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

        public async Task<List<PlayerRankCreateDTO>> AddPlayerRankWhenPlayerCreated(int playerId)
        {
            var listToReturn = new List<PlayerRankCreateDTO>();
            var skills = await _skillRepository.GetAllSkills();
            foreach (var skill in skills)
            {
                var player = new PlayerRankCreateDTO()
                {
                    PlayerId = playerId,
                    SkillId = skill.Id,
                    Rank = -1
                };
                await _repository.AddNewPlayerRank(player);
                listToReturn.Add(player);
            }

            return listToReturn;
        }

        public async Task<List<PlayerRank>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<List<PlayerRank>> GetAllPlayerRanksForPlayer(int playerId)
        {
            return await _repository.GetAllPlayerRanksForPlayer(playerId);
        }

        public async Task<PlayerRank> UpdatePlayerRank(int rankId,PlayerRankCreateDTO playerRankCreateDTO)
        {
            return await _repository.UpdatePlayerRank(rankId,playerRankCreateDTO);
        }
    }
}
