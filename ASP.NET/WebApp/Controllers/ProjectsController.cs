using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

[Route("projects")]
public class ProjectsController(IProjectService projectService) : Controller
{
  private readonly IProjectService _projectService = projectService;
  
/*   [Route("")]
  public IActionResult Projects()
  {
    return View();
  } */

  [HttpGet("")]
  public async Task<IActionResult> Projects()
  {
    var result = await _projectService.GetAllProjectsAsync();

    if (!result.Succeeded)
    {
      return View();
    }

    var model = new ProjectsViewModel
    {
      Projects = result.Result!
    };

    return View(model);
  }

  [HttpPost("add")]
  public async Task<IActionResult> Add(AddProjectViewModel model)
  {
    if (!ModelState.IsValid)
    {
      var projects = await _projectService.GetAllProjectsAsync();

      var viewModel = new ProjectsViewModel
      {
        Projects = projects.Result!,
        AddProjectViewModel = model
      };

      ViewData["ShowForm"] = "true";

      return View("Projects", viewModel);
    }

    var result = await _projectService.CreateProjectAsync(model.MapTo<AddProjectFormData>());

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    var allProjects = await _projectService.GetAllProjectsAsync();

    var errorViewModel = new ProjectsViewModel
    {
      Projects = allProjects.Result!,
      AddProjectViewModel = model
    };

    ViewData["ShowForm"] = "true";

    return View("Projects", errorViewModel);
  }

  [HttpPost("edit")]
  public async Task<IActionResult> Edit(EditProjectViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View(ModelState);
    }

    var result = await _projectService.UpdateProjectAsync(model.MapTo<EditProjectFormData>());
    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    return View();
  }
  
  [HttpPost("delete/{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    if (!ModelState.IsValid)
    {
      return View(ModelState);
    }

    var result = await _projectService.RemoveProjectAsync(id);
    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    return View();
  }
}