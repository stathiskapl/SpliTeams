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
        Task<List<PlayerRank>> GetAllPlayerRanksForUser(int userId);
        Task<List<PlayerRank>> GetAll();
        Task<bool> SavePlayerRanks(List<PlayerRankSaveDTO> playerRanksToCreateDto);
    }

    public class PlayerRankService : IPlayerRankService
    {
        private readonly IPlayerRankRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISkillRepository _skillRepository;

        public PlayerRankService(IPlayerRankRepository repository,ISkillRepository skillRepository, 
                                IUserRepository userRepository,IPlayerRepository playerRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _skillRepository = skillRepository;
            _playerRepository = playerRepository;
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

        public async Task<List<PlayerRank>> GetAllPlayerRanksForUser(int userId)
        {
            return await _repository.GetAllPlayerRanksForUser(userId);
        }

        public async Task<bool> SavePlayerRanks(List<PlayerRankSaveDTO> playerRanksToSaveDto)
        {
            //same player,user
            var user = await _userRepository.GetUserById(playerRanksToSaveDto[0].UserId);
            var player = await _playerRepository.GetPlayerById(playerRanksToSaveDto[0].PlayerId);
            foreach (var playerRank in playerRanksToSaveDto)
            {
                //Create Mode
                if (playerRank.Id == 0)
                {
                    //different skill
                    var skill = await _skillRepository.GetSkillById(playerRank.SkillId);
                    PlayerRank prank = new PlayerRank()
                    {
                        Player = player,
                        Rank = playerRank.Rank,
                        Skill = skill,
                        User = user
                    };
                    await _repository.SavePlayerRank(prank);
                }
                //Update Mode
                else
                {
                    await _repository.UpdatePlayerRankToSave(playerRank);
                }
            }
            //Saving one time in the end
            return await _repository.SaveChangesAsync();
        }

        public async Task<PlayerRank> UpdatePlayerRank(int rankId,PlayerRankCreateDTO playerRankCreateDTO)
        {
            return await _repository.UpdatePlayerRank(rankId,playerRankCreateDTO);
        }
    }
}
