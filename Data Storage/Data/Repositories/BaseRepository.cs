using Data.Contexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
	protected readonly DataContext _context = context;
	protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

	public async Task<bool> AddAsync(TEntity entity)
	{
		try
		{
			await _db.AddAsync(entity);
			await _context.SaveChangesAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ERROR: Failed to add entity.");
			Console.WriteLine($"Details: {ex.Message}");
			return false;
		}
	}
	
	public async Task<IEnumerable<TEntity>?> GetAsync()
	{
		try
		{
			var entities = await _db.ToListAsync();
			return entities;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ERROR: Failed to retrieve entities.");
			Console.WriteLine($"Details: {ex.Message}");
			return null;
		}
	}
	
	public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
	{
		try
		{
			var entity = await _db.FirstOrDefaultAsync(expression);
			return entity;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ERROR: Failed to retrieve entity.");
			Console.WriteLine($"Details: {ex.Message}");
			return null;
		}
	}
	
	public async Task<bool> UpdateAsync(TEntity entity)
	{
		try
		{
			_db.Update(entity);
			await _context.SaveChangesAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ERROR: Failed to update entity.");
			Console.WriteLine($"Details: {ex.Message}");
			return false;
		}
	}
	
	public async Task<bool> RemoveAsync(TEntity entity)
	{
		try
		{
			_db.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine("ERROR: Failed to remove entity.");
			Console.WriteLine($"Details: {ex.Message}");
			return false;
		}
	}
}