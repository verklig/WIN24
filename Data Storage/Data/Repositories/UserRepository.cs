using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{
  public async Task<IEnumerable<UserEntity>?> GetUsersAsync()
  {
    try
    {
      var entities = await _db
        .Include(x => x.Role)
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

  public async Task<UserEntity?> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
  {
    try
    {
      var entity = await _db
        .Include(x => x.Role)
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