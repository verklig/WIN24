using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
  Task<List<ProjectEntity>> GetAllProjectsWithIncludesAsync();
  Task<ProjectEntity?> GetProjectWithIncludesAsync(string id);
}

public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
  public async Task<ProjectEntity?> GetProjectWithIncludesAsync(string id)
  {
    return await _context.Projects
      .Include(p => p.ProjectUsers)
        .ThenInclude(pu => pu.User)
      .Include(p => p.Client)
      .Include(p => p.Status)
      .FirstOrDefaultAsync(p => p.Id == id);
  }

  public async Task<List<ProjectEntity>> GetAllProjectsWithIncludesAsync()
  {
    return await _context.Projects
      .Include(p => p.ProjectUsers)
        .ThenInclude(pu => pu.User)
      .Include(p => p.Client)
      .Include(p => p.Status)
      .OrderByDescending(p => p.Created)
      .ToListAsync();
  }
}