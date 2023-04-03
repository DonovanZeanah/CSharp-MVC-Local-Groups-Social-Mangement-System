using Emgu.CV.Bioinspired;
using WorkshopGroup.Models;

namespace WorkshopGroup.Repository.Interfaces
{
    // Repositories/ISkillRepository.cs
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetSkillsAsync();
        Task<Skill> GetSkillAsync(int id);
        Task<Skill> CreateSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);
        Task<Rating> CreateRatingAsync(int skillId, Rating rating);
        Task<IEnumerable<Rating>> GetRatingsAsync(int skillId);
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