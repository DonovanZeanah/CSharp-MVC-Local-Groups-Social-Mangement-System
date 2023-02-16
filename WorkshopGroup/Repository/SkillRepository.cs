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

    public async Task<IEnumerable<Skill>> GetAllSkillsByUser(string AppUserId)
    {
      return await _context.Skills.Where(a => a.AppUserId )
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
