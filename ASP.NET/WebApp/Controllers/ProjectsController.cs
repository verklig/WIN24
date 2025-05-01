using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[Authorize]
[Route("projects")]
public class ProjectsController(IProjectService projectService) : Controller
{
  private readonly IProjectService _projectService = projectService;

  #region Get Projects
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
      Projects = result.Succeeded && result.Result != null ? result.Result : [],
      AddProjectViewModel = new(),
      EditProjectViewModel = new()
    };

    return View(model);
  }
  #endregion

  #region Post Add Project
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

      ViewData["ShowAddForm"] = "true";

      return View("Projects", viewModel);
    }

    var result = await _projectService.CreateProjectAsync(model.MapTo<AddProjectFormData>());

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    var fallbackProjects = await _projectService.GetAllProjectsAsync();

    var errorViewModel = new ProjectsViewModel
    {
      Projects = fallbackProjects.Result!,
      AddProjectViewModel = model
    };

    ViewData["ShowAddForm"] = "true";

    return View("Projects", errorViewModel);
  }
  #endregion

  #region Post Edit Project
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
  #endregion

  #region Post Delete Project
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
  #endregion
}