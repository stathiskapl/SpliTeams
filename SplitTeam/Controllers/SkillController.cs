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
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly ILogger<SkillController> _log;
        public SkillController(ISkillService skillService, ILogger<SkillController> log)
        {
            _log = log;
            _skillService = skillService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<Player>> Create([FromBody] Skill skill)
        {
            try
            {
                var skillSaved = await _skillService.AddSkill(skill);
                _log.LogInformation($"Returning new skill with id {skillSaved.Id}");
                return Ok(skillSaved);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                var skills = await _skillService.GetAllSkills();
                _log.LogInformation($"Returning {skills.Count} skills");
                return Ok(skills);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
