﻿using Microsoft.AspNetCore.Mvc;
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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Player>> Create([FromBody] Player player)
        {
            try
            {
                var playerSaved = await _playerService.AddPlayer(player);
                return Ok(playerSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
