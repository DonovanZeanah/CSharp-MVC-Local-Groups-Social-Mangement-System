using WorkshopGroup.Models;

namespace WorkshopGroup.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Project>> GetAllUserProjects();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
