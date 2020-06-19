using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Services;
using Microsoft.Extensions.Logging;
using SplitTeam.Model;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ILogger<MatchController> _log;

        public MatchController(IMatchService matchService, ILogger<MatchController> log)
        {
            _matchService = matchService;
            _log = log;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<Match>> Create([FromBody] Match match)
        {
            try
            {
                var matchSaved = await _matchService.CreateMatch(match);
                _log.LogInformation($"Returning new match with id : {matchSaved.Id}");
                return Ok(matchSaved);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMatches()
        {
            try
            {
                var matches = await _matchService.GetAllMatches();
                _log.LogInformation($"Returning {matches.Count} skills");
                return Ok(matches);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Get/{matchId}")]
        public async Task<IActionResult> Get(int matchId)
        {
            try
            {
                var match = await _matchService.GetMatchById(matchId);
                _log.LogInformation($"Returning match with Id {match.Id}");
                return Ok(match);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Match match)
        {
            try
            {
                await _matchService.UpdateMatch(match);
                _log.LogInformation($"Returning updated match with id {match.Id}");
                return Ok(match);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
