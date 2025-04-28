using Domain.Models;

namespace WebApp.Models;

public class ProjectsViewModel
{
  public IEnumerable<Project> Projects { get; set; } = [];
  public AddProjectViewModel AddProjectViewModel { get; set; } = new();
  public EditProjectViewModel EditProjectViewModel { get; set; } = new();
}