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
        Task<List<Player>> GetAllPlayers();
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;

        public PlayerService(IPlayerRepository repository)
        {
            _repository = repository;
        }
        public async Task<Player> AddPlayer(Player player)
        {
            return await _repository.AddPlayer(player);
        }

        public async Task<bool> DeletePlayer(int playerId)
        {
            return await _repository.DeletePlayer(playerId);
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _repository.GetAllPlayers();
        }
    }
}
