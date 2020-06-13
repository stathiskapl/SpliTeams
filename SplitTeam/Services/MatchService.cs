
using System.Collections.Generic;
using SplitTeam.Model;
using System.Threading.Tasks;
using SplitTeam.Repositories;

namespace SplitTeam.Services
{
    public interface IMatchService
    {
        Task<Match> CreateMatch(Match match);
        Task<List<Match>> GetAllMatches();
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
    }
}
