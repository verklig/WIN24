using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Domain.Dtos;
using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IProjectService
{
  Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
  Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync();
  Task<ProjectResult<Project>> GetProjectAsync(string id);
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
  private readonly IProjectRepository _projectRepository = projectRepository;
  private readonly IStatusService _statusService = statusService;

  public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
  {
    if (formData == null)
    {
      return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var projectEntity = formData.MapTo<ProjectEntity>();
    var statusResult = await _statusService.GetStatusByIdAsync(1);
    var status = statusResult.Result;

    projectEntity.StatusId = status!.Id;

    var result = await _projectRepository.AddAsync(projectEntity);

    return result.Succeeded
    ? new ProjectResult { Succeeded = true, StatusCode = 201 }
    : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }

  public async Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync()
  {
    var response = await _projectRepository.GetAllAsync
    (
      orderByDecending: true,
      sortBy: s => s.Created,
      where: null,
      include => include.User,
      include => include.Status,
      include => include.Client
    );

    return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = response.Result };
  }

  public async Task<ProjectResult<Project>> GetProjectAsync(string id)
  {
    var response = await _projectRepository.GetAsync
    (
      where: x => x.Id == id,
      include => include.User,
      include => include.Status,
      include => include.Client
    );

    return response.Succeeded
      ? new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = response.Result }
      : new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"Project with id \"{id}\" was not found." };
  }
}