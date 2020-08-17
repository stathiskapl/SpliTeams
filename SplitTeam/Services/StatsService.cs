using SplitTeam.ModelDtos;
using SplitTeam.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Services
{
    public interface IStatsService
    {
        Task<List<PlayerStatsDto>> getPlayerStats();
    }
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;
        public StatsService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }
        public async Task<List<PlayerStatsDto>> getPlayerStats()
        {
            var playersDto = new List<PlayerStatsDto>();
            var matchesWithTeamsAndPlayers = await _statsRepository.getMatchesWithTeamsAndPlayers();
            foreach (var match in matchesWithTeamsAndPlayers) {
                foreach (var player in match.TeamPlayerA)
                {
                    playersDto.Add(new PlayerStatsDto()
                    {
                        Draws = (match.ScoreTeamA == match.ScoreTeamB) ? 1 : 0,
                        Losses = (match.ScoreTeamA < match.ScoreTeamB) ? 1 : 0,
                        Wins = (match.ScoreTeamA > match.ScoreTeamB) ? 1 : 0,
                        Player = player.Player

                    });
                }
                foreach (var player in match.TeamPlayerB)
                {
                    playersDto.Add(new PlayerStatsDto()
                    {
                        Draws = (match.ScoreTeamA == match.ScoreTeamB) ? 1 : 0,
                        Losses = (match.ScoreTeamA > match.ScoreTeamB) ? 1 : 0,
                        Wins = (match.ScoreTeamA < match.ScoreTeamB) ? 1 : 0,
                        Player = player.Player

                    });
                }
            }
            playersDto = playersDto.OrderBy(p => p.Player.Id).ToList();
            var playerId = playersDto[0].Player.Id;
            var playerLastDto = new PlayerStatsDto();
            var playerLastDtoList = new List<PlayerStatsDto>();
            foreach (var player in playersDto) {
                if (playerId == player.Player.Id) {
                    playerLastDto.Player = player.Player;
                    playerLastDto.Draws = playerLastDto.Draws + player.Draws;
                    playerLastDto.Wins = playerLastDto.Wins + player.Wins;
                    playerLastDto.Losses = playerLastDto.Losses + player.Losses;
                }
                else {
                    playerLastDtoList.Add(playerLastDto);
                    playerLastDto = new PlayerStatsDto();
                    playerId = player.Player.Id;
                    playerLastDto.Player = player.Player;
                    playerLastDto.Draws = playerLastDto.Draws + player.Draws;
                    playerLastDto.Wins = playerLastDto.Wins + player.Wins;
                    playerLastDto.Losses = playerLastDto.Losses + player.Losses;
                }
            }
            playerLastDtoList.Add(playerLastDto);
            return playerLastDtoList;
           
        }
    }
}
