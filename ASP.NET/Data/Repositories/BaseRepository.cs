using Data.Contexts;
using Data.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(AppDbContext context) where TEntity : class
{
	protected readonly AppDbContext _context = context;
	protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

	public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
	{
		if (entity == null)
		{
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
		}

		try
		{
			await _db.AddAsync(entity);
			return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201 };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
		}
	}

	public virtual RepositoryResult<bool> UpdateAsync(TEntity entity)
	{
		if (entity == null)
		{
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
		}

		try
		{
			_db.Update(entity);
			return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
		}
	}

	public virtual RepositoryResult<bool> RemoveAsync(TEntity entity)
	{
		if (entity == null)
		{
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
		}

		try
		{
			_db.Remove(entity);
			return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
		}
	}

		public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAsync()
	{
		try
		{
			var entities = await _db.ToListAsync();
			return new RepositoryResult<IEnumerable<TEntity>> { Succeeded = true, StatusCode = 200, Result = entities };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return new RepositoryResult<IEnumerable<TEntity>> { Succeeded = false, StatusCode = 500, Error = ex.Message };
		}
	}

	public virtual async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
	{
		try
		{
			var entity = await _db.FirstOrDefaultAsync(expression);
			return entity == null
				? new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 404, Error = "Entity was not found." }
				: new RepositoryResult<TEntity> { Succeeded = true, StatusCode = 200, Result = entity };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return null!;
		}
	}

	public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> expression)
	{
		try
		{
			var exists = await _db.AnyAsync(expression);
			return !exists
				? new RepositoryResult<bool> { Succeeded = false, StatusCode = 404, Error = "Entity was not found." }
				: new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
		}
		catch (Exception ex)
		{
			ShowError(ex);
			return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
		}
	}

	public virtual async Task<bool> ExecuteInTransactionAsync(Func<Task> operation)
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
		Debug.WriteLine("\nERROR: Operation failed.");
		Debug.WriteLine($"Details: {ex.Message}");
		if (ex.InnerException != null)
		{
			Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
		}
	}
}