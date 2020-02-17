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
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Player>> Create([FromBody] Skill skill)
        {
            try
            {
                var skillSaved = await _skillService.AddSkill(skill);
                return Ok(skillSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                var skills = await _skillService.GetAllSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
