using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
	private readonly ProjectRepository _projectRepository = projectRepository;

	public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
	{
		var entity = ProjectFactory.Create(form);
		if (entity == null)
		{
			return false;
		}

		return await _projectRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Project?>> GetProjectsAsync()
	{
		var entities = await _projectRepository.GetProjectsAsync();
		return entities?.Select(ProjectFactory.Create)!;
	}

	public async Task<Project?> GetProjectByIdAsync(int id)
	{
		var entity = await _projectRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return ProjectFactory.Create(entity);
	}

	public async Task<bool> UpdateProjectAsync(ProjectUpdateForm form)
	{
		var entity = await _projectRepository.GetProjectAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false;
		}

		ProjectFactory.Update(entity, form);
		return await _projectRepository.UpdateAsync(entity);
	}

	public async Task<bool> DeleteProjectAsync(int id)
	{
		var entity = await _projectRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _projectRepository.RemoveAsync(entity!);
	}
}
