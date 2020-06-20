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
        Task<List<TeamPlayer>> SplitTeams(List<int> playerIds, int teamAId, int teamBId);
        Task<List<Team>> GetAllTeams();
        Task<List<TeamPlayer>> GetAllTeamPlayersForTeamId(int teamId);
        Task<List<Team>> GetAllTeamsWithoutTeamPlayers();
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerService _playerService;
        private readonly IMatchRepository _matchRepository;
        public TeamService(ITeamRepository repository, IPlayerRepository playerRepository, IPlayerService playerService, IMatchRepository matchRepository)
        {
            _repository = repository;
            _playerRepository = playerRepository;
            _playerService = playerService;
            _matchRepository = matchRepository;
        }

        public async Task<Team> AddTeam(Team team)
        {
            return await _repository.AddTeam(team);
        }

        public async Task<List<TeamPlayer>> SplitTeams(List<int> playerIds, int teamAId, int teamBId)
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
            var teamsPlayers = CalculateEqualTeams(players,teamAId,teamBId);
            await _matchRepository.CreateMatch(new Match()
            {
                TeamA = await _repository.GetTeamById(teamAId),
                TeamB = await _repository.GetTeamById(teamBId)
            });
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

        private List<TeamPlayerDto> CalculateEqualTeams(List<Player> players, int teamAId, int teamBId)
        {
            var shuffledPlayers = players.OrderBy(a => Guid.NewGuid()).ToList();
            var teamA = shuffledPlayers.Take(shuffledPlayers.Count() / 2).Select(x =>
                new TeamPlayerDto()
                {
                    Rank = x.AverageRank.Value,
                    PlayerId = x.Id,
                    TeamId = teamAId
                }
            );
            var teamB= shuffledPlayers.Skip(shuffledPlayers.Count() / 2).Select(x =>
                new TeamPlayerDto()
                {
                    Rank = x.AverageRank.Value,
                    PlayerId = x.Id,
                    TeamId = teamBId
                }
            );
            var sumTeamA = teamA.Sum(t => t.Rank);
            var sumTeamB = teamB.Sum(t => t.Rank);
            if (Math.Abs(sumTeamA - sumTeamB) > 2)
            {
                return CalculateEqualTeams(shuffledPlayers,teamAId,teamBId);
            }
            else
            {
                return teamA.Concat(teamB).ToList();
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

        public async Task<List<Team>> GetAllTeamsWithoutTeamPlayers()
        {
            var allTeams = await _repository.GetAllTeams();
            var teamsWithPlayers = await _repository.GetAllTeamPlayers();
            var idsteamsWithPlayers = teamsWithPlayers.Select(tp => tp.Team).ToList();
            return allTeams.Where(p => !idsteamsWithPlayers.Any(p2 => p2.Id == p.Id)).ToList();
        }
    }
}
