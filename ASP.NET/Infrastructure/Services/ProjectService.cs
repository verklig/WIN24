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
  Task<ProjectResult> RemoveProjectAsync(string projectId);
  Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData);
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

    try
    {
      var projectEntity = new ProjectEntity
      {
        ProjectName = formData.ProjectName,
        Description = formData.Description,
        StartDate = formData.StartDate ?? DateTime.Today,
        EndDate = formData.EndDate,
        Budget = formData.Budget,
        ClientId = formData.ClientId,
        StatusId = formData.StatusId,
        Image = string.IsNullOrEmpty(formData.Image) ? "/images/alpha-logotype.svg" : formData.Image,
        ProjectUsers = [.. formData.UserIds.Select(userId => new UserProjectEntity
        {
          UserId = userId
        })]
      };

      var result = await _projectRepository.AddAsync(projectEntity);

      return result.Succeeded
        ? new ProjectResult { Succeeded = true, StatusCode = 201 }
        : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
    catch (Exception ex)
    {
      return new ProjectResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData)
  {
    if (formData == null)
    {
      return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var projectResult = await _projectRepository.GetAsync(x => x.Id == formData.Id);
    if (!projectResult.Succeeded || projectResult.Result == null)
    {
      return new ProjectResult { Succeeded = false, StatusCode = projectResult.StatusCode, Error = "Project not found." };
    }

    var projectEntity = projectResult.Result!.MapTo<ProjectEntity>();

    projectEntity.ProjectName = formData.ProjectName;
    projectEntity.Description = formData.Description;
    projectEntity.StartDate = formData.StartDate ?? DateTime.Today;
    projectEntity.EndDate = formData.EndDate;
    projectEntity.Budget = formData.Budget;
    projectEntity.ClientId = formData.ClientId;
    projectEntity.StatusId = formData.StatusId;
    projectEntity.Image = string.IsNullOrEmpty(formData.Image)
      ? "/images/alpha-logotype.svg"
      : formData.Image;

    projectEntity.ProjectUsers = [.. formData.UserIds.Select(userId => new UserProjectEntity
    {
      UserId = userId,
      ProjectId = projectEntity.Id
    })];

    var result = await _projectRepository.UpdateAsync(projectEntity);

    return result.Succeeded
      ? new ProjectResult { Succeeded = true, StatusCode = 200 }
      : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }

  public async Task<ProjectResult> RemoveProjectAsync(string projectId)
  {
    if (string.IsNullOrEmpty(projectId))
    {
      return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Project ID cannot be null or empty." };
    }

    var projectResult = await _projectRepository.GetAsync(x => x.Id == projectId);
    if (!projectResult.Succeeded)
    {
      return new ProjectResult { Succeeded = false, StatusCode = projectResult.StatusCode, Error = "Project could not found." };
    }

    var projectEntity = projectResult.Result!.MapTo<ProjectEntity>();
    var result = await _projectRepository.RemoveAsync(projectEntity);

    return result.Succeeded
      ? new ProjectResult { Succeeded = true, StatusCode = 200 }
      : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
  }

  public async Task<ProjectResult<IEnumerable<Project>>> GetAllProjectsAsync()
  {
    var entities = await _projectRepository.GetAllProjectsWithIncludesAsync();

    var projects = entities.Select(p => new Project
    {
      Id = p.Id,
      ProjectName = p.ProjectName,
      Description = p.Description,
      StartDate = p.StartDate,
      EndDate = p.EndDate,
      Created = p.Created,
      Budget = p.Budget,
      Image = p.Image,
      Client = new Client
      {
        Id = p.Client!.Id,
        ClientName = p.Client.ClientName
      },
      Status = new Status
      {
        Id = p.Status!.Id,
        StatusName = p.Status.StatusName
      },
      Users = [.. p.ProjectUsers
        .Where(pu => pu.User != null)
        .Select(pu => new User
        {
          Id = pu.User.Id,
          FirstName = pu.User.FirstName!,
          LastName = pu.User.LastName!,
          Email = pu.User.Email!,
          Image = pu.User.Image
        })]
    });

    return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = projects };
  }

  public async Task<ProjectResult<Project>> GetProjectAsync(string id)
  {
    var entity = await _projectRepository.GetProjectWithIncludesAsync(id);

    if (entity == null)
    {
      return new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"Project with id \"{id}\" was not found." };
    }

    var project = new Project
    {
      Id = entity.Id,
      ProjectName = entity.ProjectName,
      Description = entity.Description,
      StartDate = entity.StartDate,
      EndDate = entity.EndDate,
      Created = entity.Created,
      Budget = entity.Budget,
      Image = entity.Image,
      Client = new Client
      {
        Id = entity.Client!.Id,
        ClientName = entity.Client.ClientName
      },
      Status = new Status
      {
        Id = entity.Status!.Id,
        StatusName = entity.Status.StatusName
      },
      Users = [.. entity.ProjectUsers
        .Where(pu => pu.User != null)
        .Select(pu => new User
        {
          Id = pu.User.Id,
          FirstName = pu.User.FirstName!,
          LastName = pu.User.LastName!,
          Email = pu.User.Email!,
          Image = pu.User.Image
        })]
    };

    return new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = project };
  }
}