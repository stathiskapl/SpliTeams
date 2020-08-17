using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SplitTeam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StatController : ControllerBase
    {
        private readonly ILogger<StatController> _log;
        private readonly IStatsService _service;
        public StatController(ILogger<StatController> log, IStatsService service)
        {
            _log = log;
            _service = service;
        }
        [HttpGet("GetMatchesWithTeamsAndPlayers")]
        public async Task<IActionResult> GetMatchesWithTeamsAndPlayers()
        {
            try
            {
                var stats = await _service.getPlayerStats();
                _log.LogInformation($"Returning {stats.Count} playerStats");
                return Ok(stats);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

    }
}
