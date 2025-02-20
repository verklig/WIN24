using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
	private readonly ProjectRepository _projectRepository = projectRepository;
	
	public async Task CreateProjectAsync(ProjectRegistrationForm form)
	{
		var entity = ProjectFactory.Create(form)!;
		await _projectRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Project>> GetProjectsAsync()
	{
		var entity = await _projectRepository.GetAsync();
		return entity.Select(ProjectFactory.Create)!;
	}

	public async Task<Project> GetProjectByIdAsync(int id)
	{
		var entity = await _projectRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Project not found");
		return ProjectFactory.Create(entity!)!;
	}

	public async Task UpdateProjectAsync(ProjectUpdateForm form)
	{
		var entity = await _projectRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Project not found");

		ProjectFactory.Update(entity, form);

		await _projectRepository.UpdateAsync(entity);
	}

	public async Task DeleteProjectAsync(int id)
	{
		var entity = await _projectRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Project not found");
		await _projectRepository.RemoveAsync(entity!);
	}
}