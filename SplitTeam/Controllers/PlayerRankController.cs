using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Model;
using SplitTeam.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerRankController : ControllerBase
    {
        private readonly ILogger<PlayerRankController> _log;
        private readonly IPlayerRankService _playerRankService;

        public PlayerRankController(IPlayerRankService playerRankService, ILogger<PlayerRankController> log)
        {
            _log = log;
            _playerRankService = playerRankService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<PlayerRank>> Create([FromBody] PlayerRankCreateDTO playerRankCreateDto)
        {
            try
            {
                var playerRankSaved = await _playerRankService.AddPlayerRank(playerRankCreateDto);
                _log.LogInformation($"Returning new playerRank with id {playerRankSaved.Id}");
                if (playerRankSaved != null)
                {
                    return Ok(playerRankSaved);
                }
                else
                {
                    return BadRequest("Cannot insert Rank for already existed skill");
                }

            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAllForPlayer/{playerId}")]
        public async Task<IActionResult> GetAllPlayerRanksForPlayer(int playerId)
        {
            try
            {
                var players = await _playerRankService.GetAllPlayerRanksForPlayer(playerId);
                _log.LogInformation($"Returning {players.Count} playerRanks for player with id {playerId}");
                return Ok(players);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAllForUser/{userId}")]
        public async Task<IActionResult> GetAllPlayerRanksForUser(int userId)
        {
            try
            {
                var playerRanks = await _playerRankService.GetAllPlayerRanksForUser(userId);
                _log.LogInformation($"Returning {playerRanks.Count} playerRanks for player with id {userId}");
                return Ok(playerRanks);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var players = await _playerRankService.GetAll();
                _log.LogInformation($"Returning {players.Count} playerRanks");
                return Ok(players);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpPut("Update/{rankId}")]
        public async Task<IActionResult> Update(int rankId,[FromBody] PlayerRankCreateDTO playerRankCreateDTO)
        {
            try
            {
                var playerRanks = await _playerRankService.UpdatePlayerRank(rankId,playerRankCreateDTO);
                _log.LogInformation($"Updating playerRank with id : {playerRanks.Id}");
                return Ok(playerRanks);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpPut("SavePlayerRanks")]
        public async Task<IActionResult> SavePlayerRanks([FromBody] List<PlayerRankSaveDTO> playerRanksSaveDTO)
        {
            try
            {
                var playerRanks = await _playerRankService.SavePlayerRanks(playerRanksSaveDTO);
                _log.LogInformation($"Success Saving : {playerRanks} ");
                return Ok(playerRanks);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
