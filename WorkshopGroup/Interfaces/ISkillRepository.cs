using Emgu.CV.Bioinspired;
using WorkshopGroup.Models;

namespace WorkshopGroup.Interfaces
{
    // Repositories/ISkillRepository.cs
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetSkills();
        Task<Skill> GetSkill(int id);
        Task<Skill> CreateSkill(Skill skill);
        Task UpdateSkill(Skill skill);
        Task DeleteSkill(int id);
        Task<Rating> CreateRating(int skillId, Rating rating);
        Task<IEnumerable<Rating>> GetRatings(int skillId);
    }
}
 /* public interface ISkillRepository
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
*/