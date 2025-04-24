using Infrastructure.Models;
using Data.Repositories;
using Domain.Extensions;

namespace Infrastructure.Services;

public interface IStatusService
{
  Task<StatusResult> GetStatusResultAsync();
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
  private readonly IStatusRepository _statusRepository = statusRepository;
  
  public async Task<StatusResult> GetStatusResultAsync()
  {
    var result = await _statusRepository.GetAllAsync();
    return result.MapTo<StatusResult>();
  }
}