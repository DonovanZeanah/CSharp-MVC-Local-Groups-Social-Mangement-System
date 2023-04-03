using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;

namespace WorkshopGroup.Repository
{
    public class ProjectRepository : IProjectRepository
  {
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context) 
    { 
      _context = context;
    }

    public bool Add(Project project)
    {
      _context.Add(project);
      return Save();
    }

    public bool Delete(Project project)
    {
      _context.Remove(project);
      return Save();
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
      return await _context.Projects.ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetAllProjectsByCity(string city)
    {
      return await _context.Projects.Where(c => c.Address.City.Contains(city)).ToListAsync();
    }

    public async Task<Project> GetByIdAsync(int id)
    {
      return await _context.Projects.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Project> GetByIdAsyncNoTracking(int id)
    {
      return await _context.Projects.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
    }

    public bool Save()
    {
      var saved = _context.SaveChanges();
      return saved > 0 ? true : false;
    }

    public bool Save(Project project)
    {
      throw new NotImplementedException();
    }

    public bool Update(Project project)
    {
      _context.Update(project);
      return Save();
    }
  }
}
