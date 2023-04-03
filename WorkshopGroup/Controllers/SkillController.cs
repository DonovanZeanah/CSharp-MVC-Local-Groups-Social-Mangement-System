using Emgu.CV.Bioinspired;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;

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
        /// <summary>
        /// Retrieve all skills.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return Ok(await _skillRepository.GetSkillsAsync());
        }

        // GET: api/skill/{id}
        /// <summary>
        /// Retrieve a skill.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _skillRepository.GetSkillAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // POST: api/skill
        /// <summary>
        /// Create a skill.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(Skill skill)
        {
            
            await _skillRepository.CreateSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }
        /*
          
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
         */

        // PUT: api/skill/{id}
        /// <summary>
        /// Update a skill.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            await _skillRepository.UpdateSkillAsync(skill);
            return NoContent();
        }

        // DELETE: api/skill/{id}
        /// <summary>
        /// Delete a skill.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await _skillRepository.DeleteSkillAsync(id);
            return NoContent();
        }

        // POST: api/skill/{id}/rating
        /// <summary>
        /// Rate a skill.
        /// </summary>
        [HttpPost("{id}/rating")]
        public async Task<ActionResult<Rating>> CreateRating(int id, Rating rating)
        {
            rating.SkillId = id;
            await _skillRepository.CreateRatingAsync(id, rating);
            return CreatedAtAction(nameof(GetSkill), new { id = rating.Id }, rating);
        }

        // GET: api/skill/{id}/rating
        /// <summary>
        /// Retrieve a skill rating.
        /// </summary>
        [HttpGet("{id}/rating")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings(int id)
        {
            return Ok(await _skillRepository.GetRatingsAsync(id));
        }
    }
}

