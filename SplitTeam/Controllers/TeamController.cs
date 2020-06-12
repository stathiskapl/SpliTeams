using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Services;
using Microsoft.AspNetCore.Authorization;
using SplitTeam.Model;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(Roles = "Admin")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _log;
        private readonly ITeamService _teamService;
        public TeamController(ILogger<TeamController> log, ITeamService teamService)
        {
            _log = log;
            _teamService = teamService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            try
            {
                var teamSaved = await _teamService.AddTeam(team);
                _log.LogInformation($"Returning new team with id {teamSaved.Id}");
                return Ok(teamSaved);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpPost("SplitTeams")]
        public async Task<IActionResult> SplitTeams([FromBody] List<int> playerIds)
        {
            try
            {
                var isok = await _teamService.SplitTeams(playerIds);
                //_log.LogInformation($"Returning new team with id {teamSaved.Id}");
                return Ok(isok);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
