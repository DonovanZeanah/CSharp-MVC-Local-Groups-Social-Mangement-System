using WorkshopGroup.Models;

namespace WorkshopGroup.Interfaces
{
  public interface ISkillRepository
  {
    Task<IEnumerable<Skill>> GetAll();
    Task<Skill> GetByIdAsync(int id);
    Task<Skill> GetByIdAsyncNoTracking(int id);
    Task<IEnumerable<Skill>> GetAllSkillsByUser (string AppUserId);

    bool Add(Skill skill);
    bool Update(Skill skill);
    bool Delete(Skill skill);
    bool Save();

    bool Save(Skill skill);
  }
}
