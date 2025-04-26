using Infrastructure.Models;
using Data.Repositories;
using Domain.Models;

namespace Infrastructure.Services;

public interface IStatusService
{
  Task<StatusResult<IEnumerable<Status>>> GetAllStatusesAsync();
  Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
  Task<StatusResult<Status>> GetStatusByIdAsync(int statusId);
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
  private readonly IStatusRepository _statusRepository = statusRepository;

  public async Task<StatusResult<IEnumerable<Status>>> GetAllStatusesAsync()
  {
    var result = await _statusRepository.GetAllAsync();
    return result.Succeeded
      ? new StatusResult<IEnumerable<Status>> { Succeeded = true, StatusCode = 200, Result = result.Result }
      : new StatusResult<IEnumerable<Status>> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }

  public async Task<StatusResult<Status>> GetStatusByNameAsync(string statusName)
  {
    var result = await _statusRepository.GetAsync(x => x.StatusName == statusName);
    return result.Succeeded
      ? new StatusResult<Status> { Succeeded = true, StatusCode = 200, Result = result.Result }
      : new StatusResult<Status> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }

  public async Task<StatusResult<Status>> GetStatusByIdAsync(int statusId)
  {
    var result = await _statusRepository.GetAsync(x => x.Id == statusId);
    return result.Succeeded
      ? new StatusResult<Status> { Succeeded = true, StatusCode = 200, Result = result.Result }
      : new StatusResult<Status> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }
}