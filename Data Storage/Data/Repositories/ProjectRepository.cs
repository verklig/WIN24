using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
  public async Task<IEnumerable<ProjectEntity>?> GetProjectsAsync()
  {
    try
    {
      var entities = await _db
        .Include(x => x.Status)
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.User)
        .ToListAsync();

      return entities;
    }
    catch (Exception ex)
    {
      Console.WriteLine("\nERROR: Failed to retrieve entities.");
      Console.WriteLine($"Details: {ex.Message}");
      if (ex.InnerException != null)
      {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
      }

      return null;
    }
  }

  public async Task<ProjectEntity?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
  {
    try
    {
      var entity = await _db
        .Include(x => x.Status)
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.User)
        .FirstOrDefaultAsync(expression);

      return entity;
    }
    catch (Exception ex)
    {
      Console.WriteLine("\nERROR: Failed to retrieve entity.");
      Console.WriteLine($"Details: {ex.Message}");
      if (ex.InnerException != null)
      {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
      }

      return null;
    }
  }
}