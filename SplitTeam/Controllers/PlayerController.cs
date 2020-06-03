using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SplitTeam.Model;
using SplitTeam.Services;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerController> _log;

        public PlayerController(IPlayerService playerService, ILogger<PlayerController> log)
        {
            _log = log;
            _playerService = playerService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Player>> Create([FromBody] Player player)
        {
            try
            {
                var playerSaved = await _playerService.AddPlayer(player);
                _log.LogInformation($"Returning {playerSaved.Name}");
                return Ok(playerSaved);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _playerService.DeletePlayer(id);
                _log.LogInformation($"Deleted player with id {id} : {isDeleted}");
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("DeleteWithRanks/{id}")]
        public async Task<IActionResult> DeleteWithRanks(int id)
        {
            try
            {
                var isDeleted = await _playerService.DeletePlayerWithRanks(id);
                _log.LogInformation($"Deleted player with with ranks with id {id} : {isDeleted}");
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBetsForUser()
        {
            try
            {

                var players = await _playerService.GetAllPlayers();
                _log.LogInformation($"Returning {players.Count} players");
                return Ok(players);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
