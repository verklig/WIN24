using Data.Contexts;
using Data.Models;
using Domain.Extensions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public interface IBaseRepository<TEntity, T> where TEntity : class
{
  Task<RepositoryResult<bool>> AddAsync(TEntity entity);
  Task<bool> ExecuteInTransactionAsync(Func<Task> operation);
  Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> expression);
  Task<RepositoryResult<IEnumerable<T>>> GetAllAsync(bool orderByDecending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
  Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDecending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
  Task<RepositoryResult<T>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
  Task<RepositoryResult<TEntity>> GetEntityAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
  Task<RepositoryResult<bool>> RemoveAsync(TEntity entity);
  Task<RepositoryResult<bool>> UpdateAsync(TEntity entity);
}

public abstract class BaseRepository<TEntity, T>(AppDbContext context) : IBaseRepository<TEntity, T> where TEntity : class
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
      await _context.SaveChangesAsync();
      return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201 };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
  {
    if (entity == null)
    {
      return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
    }

    try
    {
      _db.Update(entity);
      await _context.SaveChangesAsync();
      return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public virtual async Task<RepositoryResult<bool>> RemoveAsync(TEntity entity)
  {
    if (entity == null)
    {
      return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
    }

    try
    {
      _db.Remove(entity);
      await _context.SaveChangesAsync();
      return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public virtual async Task<RepositoryResult<IEnumerable<T>>> GetAllAsync(bool orderByDecending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
  {
    try
    {
      IQueryable<TEntity> query = _db;

      if (where != null)
      {
        query = query.Where(where);
      }

      if (includes != null && includes.Length != 0)
      {
        foreach (var include in includes)
        {
          query = query.Include(include);
        }
      }

      if (sortBy != null)
      {
        query = orderByDecending
          ? query.OrderByDescending(sortBy)
          : query.OrderBy(sortBy);
      }

      var entities = await query.ToListAsync();

      if (entities == null)
      {
        return new RepositoryResult<IEnumerable<T>> { Succeeded = false, StatusCode = 404, Error = "No entities found." };
      }

      var result = entities.Select(entity => entity.MapTo<T>());
      return new RepositoryResult<IEnumerable<T>> { Succeeded = true, StatusCode = 200, Result = result };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<IEnumerable<T>> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDecending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
  {
    try
    {
      IQueryable<TEntity> query = _db;

      if (where != null)
      {
        query = query.Where(where);
      }

      if (includes != null && includes.Length != 0)
      {
        foreach (var include in includes)
        {
          query = query.Include(include);
        }
      }

      if (sortBy != null)
      {
        query = orderByDecending
          ? query.OrderByDescending(sortBy)
          : query.OrderBy(sortBy);
      }

      var entities = await query.Select(selector).ToListAsync();
      var result = entities.Select(entity => entity!.MapTo<TSelect>());
      return new RepositoryResult<IEnumerable<TSelect>> { Succeeded = true, StatusCode = 200, Result = result };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<IEnumerable<TSelect>> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public virtual async Task<RepositoryResult<T>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
  {
    try
    {
      IQueryable<TEntity> query = _db;

      if (includes != null && includes.Length != 0)
      {
        foreach (var include in includes)
        {
          query = query.Include(include);
        }
      }

      var entity = await query.FirstOrDefaultAsync(where);

      if (entity == null)
      {
        return new RepositoryResult<T> { Succeeded = false, StatusCode = 404, Error = "Entity was not found." };
      }

      var result = entity.MapTo<T>();
      return new RepositoryResult<T> { Succeeded = true, StatusCode = 200, Result = result };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<T> { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }
  public virtual async Task<RepositoryResult<TEntity>> GetEntityAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
  {
    try
    {
      IQueryable<TEntity> query = _db;

      if (includes != null && includes.Length > 0)
      {
        foreach (var include in includes)
        {
          query = query.Include(include);
        }
      }

      var entity = await query.FirstOrDefaultAsync(where);
      if (entity == null)
      {
        return new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 404, Error = "Entity was not found." };
      }

      return new RepositoryResult<TEntity> { Succeeded = true, StatusCode = 200, Result = entity };
    }
    catch (Exception ex)
    {
      ShowError(ex);
      return new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 500, Error = ex.Message };
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