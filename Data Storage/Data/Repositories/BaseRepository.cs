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
			return true;
		}
		catch (Exception ex)
		{
			ShowError(ex);
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
			ShowError(ex);
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
			ShowError(ex);
			return null;
		}
	}

	public Task<bool> UpdateAsync(TEntity entity)
	{
		try
		{
			_db.Update(entity);
			return Task.FromResult(true);
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return Task.FromResult(false);
		}
	}

	public Task<bool> RemoveAsync(TEntity entity)
	{
		try
		{
			_db.Remove(entity);
			return Task.FromResult(true);
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return Task.FromResult(false);
		}
	}

	public async Task<bool> ExecuteInTransactionAsync(Func<Task> operation)
	{
		using var transaction = await _context.Database.BeginTransactionAsync();

		try
		{
			await operation();
			await _context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception)
		{
			await transaction.RollbackAsync();
			return false;
			throw;
		}
	}

	private void ShowError(Exception ex)
	{
		Console.WriteLine("\nERROR: Operation failed.");
		Console.WriteLine($"Details: {ex.Message}");
		if (ex.InnerException != null)
		{
			Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
		}
	}
}