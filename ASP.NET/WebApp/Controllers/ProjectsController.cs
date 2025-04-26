using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

[Route("")]
public class ProjectsController(IProjectService projectService) : Controller
{
  private readonly IProjectService _projectService = projectService;

  [Route("projects")]
  public IActionResult Projects()
  {
    return View();
  }

  // [Route("projects")]
  // public async Task<IActionResult> Projects()
  // {
  //   var model = new ProjectsViewModel
  //   {
  //     Projects = await _projectService.GetAllProjectsAsync()
  //   };

  //   return View(model);
  // }

  // [HttpPost]
  // public async Task<IActionResult> Create(AddProjectViewModel model)
  // {
  //   var addProjectFormData = model.MapTo<AddProjectFormData>;

  //   var result = await _projectService.CreateProjectAsync(addProjectFormData);

  //   return Json(new { });
  // }

  // [HttpPost]
  // public async Task<IActionResult> Edit(EditProjectViewModel model)
  // {
  //   var editProjectFormData = model.MapTo<EditProjectViewModel>;

  //   var result = await _projectService.EditProjectAsync(editProjectFormData);

  //   return Json(new { });
  // }
  
  // [HttpPost]
  // public async Task<IActionResult> Delete()
  // {
  //   var result = await _projectService.DeleteProjectAsync();

  //   return Json(new { });
  // }
}