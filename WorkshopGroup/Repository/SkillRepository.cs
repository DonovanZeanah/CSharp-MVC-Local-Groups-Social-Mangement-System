using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;

namespace WorkshopGroup.Repository
{
    public class SkillRepository : ISkillRepository
    {

        // Repositories/SkillRepository.cs
        public Task<Rating> CreateRating(int skillId, Rating rating)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> CreateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rating>> GetRatings(int skillId)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> GetSkill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Skill>> GetSkills()
        {
            throw new NotImplementedException();
        }

        public Task UpdateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
   /* private readonly ApplicationDbContext _context;
    public SkillRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public bool Add(Skill skill)
    {
      _context.Add(skill);
      return Save();
    }

    public bool Delete(Skill skill)
    {
      _context.Remove(skill);
      return Save();
    }

    public async Task<IEnumerable<Skill>> GetAll()
    {
      return await _context.Skills.ToListAsync();
    }

    public Task<IEnumerable<Skill>> GetAllSkillsByUser(string AppUserId)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Skill>> GetAllSkillsByUserId(string AppUserId)
    {
      throw new NotImplementedException();
    }

    public Task<Skill> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Skill> GetByIdAsyncNoTracking(int id)
    {
      throw new NotImplementedException();
    }

    public bool Save()
    {
      throw new NotImplementedException();
    }

    public bool Save(Skill skill)
    {
      throw new NotImplementedException();
    }

    public bool Update(Skill skill)
    {
      throw new NotImplementedException();
    }
  }
}
*/