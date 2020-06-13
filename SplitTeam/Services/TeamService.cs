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
        Task<List<TeamPlayer>> SplitTeams(List<int> playerIds);
        Task<List<Team>> GetAllTeams();
        Task<List<TeamPlayer>> GetAllTeamPlayersForTeamId(int teamId);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerService _playerService;
        public TeamService(ITeamRepository repository, IPlayerRepository playerRepository, IPlayerService playerService)
        {
            _repository = repository;
            _playerRepository = playerRepository;
            _playerService = playerService;
        }

        public async Task<Team> AddTeam(Team team)
        {
            return await _repository.AddTeam(team);
        }

        public async Task<List<TeamPlayer>> SplitTeams(List<int> playerIds)
        {
            foreach (var playerId in playerIds)
            {
                await _playerService.CalculateAverageRankForPlayer(playerId);
            }
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
            //Teams are ok
            var teamsPlayers = CalculateEqualTeams(players);
            return await SaveTeamPlayers(teamsPlayers);
        }

        private async Task<List<TeamPlayer>> SaveTeamPlayers(List<TeamPlayerDto> teamsPlayers)
        {
            var teamPlayers = new List<TeamPlayer>();
            foreach (var teamsPlayer in teamsPlayers)
            {
                TeamPlayer teamPlayer = new TeamPlayer()
                {
                    Player = await _playerRepository.GetPlayerById(teamsPlayer.PlayerId),
                    Team = await _repository.GetTeamById(teamsPlayer.TeamId)
                };
                teamPlayers.Add(teamPlayer);
            }
            return await _repository.SaveTeamPlayers(teamPlayers);
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

        public async Task<List<Team>> GetAllTeams()
        {
            return await _repository.GetAllTeams();
        }

        public async Task<List<TeamPlayer>> GetAllTeamPlayersForTeamId(int teamId)
        {
            return await _repository.GetAllTeamPlayersForTeamId(teamId);
        }
    }
}
