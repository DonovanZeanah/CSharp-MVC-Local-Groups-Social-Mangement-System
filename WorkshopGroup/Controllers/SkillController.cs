using Emgu.CV.Bioinspired;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Interfaces;

namespace WorkshopGroup.Controllers
{
    // Controllers/SkillController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        // GET: api/skill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return Ok(await _skillRepository.GetSkills());
        }

        // GET: api/skill/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _skillRepository.GetSkill(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // POST: api/skill
        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(Skill skill)
        {
            await _skillRepository.CreateSkill(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // PUT: api/skill/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            await _skillRepository.UpdateSkill(skill);
            return NoContent();
        }

        // DELETE: api/skill/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await _skillRepository.DeleteSkill(id);
            return NoContent();
        }

        // POST: api/skill/{id}/rating
        [HttpPost("{id}/rating")]
        public async Task<ActionResult<Rating>> CreateRating(int id, Rating rating)
        {
            rating.SkillId = id;
            await _skillRepository.CreateRating(id, rating);
            return CreatedAtAction(nameof(GetSkill), new { id = rating.Id }, rating);
        }

        // GET: api/skill/{id}/rating
        [HttpGet("{id}/rating")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings(int id)
        {
            return Ok(await _skillRepository.GetRatings(id));
        }
    }
}

