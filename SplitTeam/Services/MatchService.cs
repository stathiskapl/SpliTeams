
using System.Collections.Generic;
using SplitTeam.Model;
using System.Threading.Tasks;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface IMatchService
    {
        Task<Match> UpdateMatch(Match match);
        Task<Match> CreateMatch(Match match);
        Task<List<Match>> GetAllMatches();
        Task<Match> GetMatchById(int matchId);

    }

    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repository;

        public MatchService(IMatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Match> CreateMatch(Match match)
        {
            return await _repository.CreateMatch(match);
        }

        public async Task<List<Match>> GetAllMatches()
        {
            return await _repository.GetAllMatches();
        }

        public async Task<Match> GetMatchById(int matchId)
        {
            return await _repository.GetMatchById(matchId);
        }

        public async Task<Match> UpdateMatch(Match match)
        {
            return await _repository.UpdateMatch(match);
        }
    }
}
