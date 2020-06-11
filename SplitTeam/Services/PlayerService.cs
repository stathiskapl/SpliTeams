using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface IPlayerService
    {
        Task<Player> AddPlayer(Player player);
        Task<bool> DeletePlayer(int playerId);
        Task<bool> DeletePlayerWithRanks(int playerId);
        Task<List<Player>> GetAllPlayers();
        Task<Player> CalculateAverageRankForPlayer(int playerId);
        Task<Player> saveAverageRank(int playerId,decimal averageRank);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly IPlayerRankService _playerRankService;

        public PlayerService(IPlayerRepository repository, IPlayerRankService playerRankService)
        {
            _playerRankService = playerRankService;
            _repository = repository;
        }
        public async Task<Player> AddPlayer(Player player)
        {

            await _repository.AddPlayer(player);
            //await _playerRankService.AddPlayerRankWhenPlayerCreated(player.Id);
            return player;
        }

        public async Task<Player> CalculateAverageRankForPlayer(int playerId)
        {
            var playerRanksForPlayer = await _playerRankService.GetAllPlayerRanksForPlayer(playerId);
            int sum = 0;
            foreach (var playerRankForPlayer in playerRanksForPlayer)
            {
                sum = sum + playerRankForPlayer.Rank;
            }
            var avgRank = decimal.Divide(sum, playerRanksForPlayer.Count);
            return await saveAverageRank(playerId, avgRank);
        }

        public async Task<bool> DeletePlayer(int playerId)
        {
            return await _repository.DeletePlayer(playerId);
        }

        public async Task<bool> DeletePlayerWithRanks(int playerId)
        {
            return await _repository.DeletePlayerWithRanks(playerId);
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _repository.GetAllPlayers();
        }

        public async Task<Player> saveAverageRank(int playerId, decimal averageRank)
        {
            var playerToUpdate = await _repository.GetPlayerById(playerId);
            playerToUpdate.AverageRank = averageRank;
            return await _repository.UpdatePlayer(playerToUpdate);
        }
    }
}
