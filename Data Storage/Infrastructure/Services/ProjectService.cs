using Data.Repositories;

namespace Infrastructure.Services;

public class ProjectService(ProjectRepository projectRepository)
{
	private readonly ProjectRepository _projectRepository = projectRepository;
}