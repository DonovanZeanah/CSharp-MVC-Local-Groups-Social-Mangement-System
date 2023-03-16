using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;

namespace WorkshopGroup.Repository
{
    public class SkillRepository : ISkillRepository
    {





        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Skill> GetSkillAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _context.Skills.Include(s => s.Ratings).ToListAsync();
        }

        public async Task<Skill> GetSkill(int id)
        {
            return await _context.Skills.Include(s => s.Ratings).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill> CreateSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            _context.Entry(skill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Rating> CreateRatingAsync(int skillId, Rating rating)
        {
            var skill = await _context.Skills.FindAsync(skillId);
            if (skill != null)
            {
                rating.SkillId = skillId;
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
                return rating;
            }
            return null;
        }

        public async Task<IEnumerable<Rating>> GetRatingsAsync(int skillId)
        {
            return await _context.Ratings.Where(r => r.SkillId == skillId).ToListAsync();
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