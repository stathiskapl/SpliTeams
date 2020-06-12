using SplitTeam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.ModelDtos;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface ITeamService
    {
        Task<Team> AddTeam(Team team);
        Task<bool> SplitTeams(List<int> playerIds);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        public TeamService(ITeamRepository repository, IPlayerRepository playerRepository)
        {
            _repository = repository;
            _playerRepository = playerRepository;
        }

        public async Task<Team> AddTeam(Team team)
        {
            return await _repository.AddTeam(team);
        }

        public async Task<bool> SplitTeams(List<int> playerIds)
        {
            var players = new List<Player>();
            decimal sum = 0;
            foreach (var playerId in playerIds)
            {
                var player = await _playerRepository.GetPlayerById(playerId);
                players.Add(player);
                if (player.AverageRank != null)
                {
                    sum = sum + player.AverageRank.Value;
                }

            }
            var a = CalculateEqualTeams(players);
            var b = a;
            return true;
        }
        private List<TeamPlayerDto> CalculateEqualTeams(List<Player> players)
        {
            var shuffledPlayers = players.OrderBy(a => Guid.NewGuid()).ToList();
            var listToReturn = new List<TeamPlayerDto>();
            var teamA = new List<TeamPlayerDto>();
            var teamB= new List<TeamPlayerDto>();
            //FillTeams
            for (int i = 0; i < players.Count; i++)
            {
                if (i % 2 == 0)
                {
                    teamA.Add(new TeamPlayerDto()
                    {
                        Rank = players[i].AverageRank.Value,
                        PlayerId = players[i].Id,
                        TeamId = 1
                    });
                }
                else
                {
                    teamB.Add(new TeamPlayerDto()
                    {
                        Rank = players[i].AverageRank.Value,
                        PlayerId = players[i].Id,
                        TeamId = 2
                    });
                }
            }
            decimal sumTeamA = 0;
            foreach (var playerA in teamA)
            {
                sumTeamA = sumTeamA + playerA.Rank;
                listToReturn.Add(playerA);
            }
            decimal sumTeamB = 0;
            foreach (var playerB in teamB)
            {
                sumTeamB = sumTeamB + playerB.Rank;
                listToReturn.Add(playerB);
            }
            if (Math.Abs(sumTeamA - sumTeamB) > 2)
            {
                return CalculateEqualTeams(shuffledPlayers);
            }
            else
            {
                return listToReturn;
            }
        }
    }
}
