using WorkshopGroup.Models;

namespace WorkshopGroup.Interfaces
{
  public interface IProjectRepository
  {
    Task<IEnumerable<Project>> GetAll();
    Task<Project> GetByIdAsync(int id);
    Task<IEnumerable<Project>> GetAllProjectsByCity(string city);
    bool Add(Project project);
    bool Update(Project project);
    bool Delete(Project project);
    bool Save();

    bool Save(Project project);
  }
}
