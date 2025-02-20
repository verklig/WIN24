using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IProjectService
{
	Task CreateProjectAsync(ProjectRegistrationForm form);
	Task<IEnumerable<Project>> GetProjectsAsync();
	Task<Project> GetProjectByIdAsync(int id);
	Task UpdateProjectAsync(ProjectUpdateForm form);
	Task DeleteProjectAsync(int id);
}