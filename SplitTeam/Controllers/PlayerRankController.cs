using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SplitTeam.Model;
using SplitTeam.Services;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerRankController : ControllerBase
    {
        private readonly IPlayerRankService _playerRankService;

        public PlayerRankController(IPlayerRankService playerRankService)
        {
            _playerRankService = playerRankService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<PlayerRank>> Create([FromBody] PlayerRankCreateDTO playerRankCreateDto)
        {
            try
            {
                var playerRankSaved = await _playerRankService.AddPlayerRank(playerRankCreateDto);
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
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAllForPlayer/{playerId}")]
        public async Task<IActionResult> GetAllPlayerRanksForPlayer(int playerId)
        {
            try
            {
                var players = await _playerRankService.GetAllPlayerRanksForPlayer(playerId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
