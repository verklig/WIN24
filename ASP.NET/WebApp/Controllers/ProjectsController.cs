using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;

namespace Webapp.Controllers;

[Authorize]
[Route("projects")]
public class ProjectsController(IProjectService projectService, IStatusService statusService, IClientService clientService, IUserService userService, IImageService imageService) : Controller
{
  private readonly IProjectService _projectService = projectService;
  private readonly IStatusService _statusService = statusService;
  private readonly IClientService _clientService = clientService;
  private readonly IUserService _userService = userService;
  private readonly IImageService _imageService = imageService;

  #region Get Projects
  [HttpGet("")]
  public async Task<IActionResult> Projects(string filter)
  {
    var projectResult = await _projectService.GetAllProjectsAsync();
    var statusResult = await _statusService.GetAllStatusesAsync();
    var clientResult = await _clientService.GetAllClientsAsync();
    var userResult = await _userService.GetAllUsersAsync();

    if (!projectResult.Succeeded || !statusResult.Succeeded || !clientResult.Succeeded || !userResult.Succeeded)
    {
      return View();
    }

    var allProjects = projectResult.Result!;

    var allCount = allProjects.Count();
    var startedCount = allProjects.Count(p => p.Status?.StatusName == "Started");
    var completedCount = allProjects.Count(p => p.Status?.StatusName == "Completed");

    var filteredProjects = allProjects;

    if (filter == "started")
    {
      filteredProjects = [.. filteredProjects.Where(p => p.Status?.StatusName == "Started")];
    }
    else if (filter == "completed")
    {
      filteredProjects = [.. filteredProjects.Where(p => p.Status?.StatusName == "Completed")];
    }

    var model = new ProjectsViewModel
    {
      Projects = filteredProjects,
      AddProjectViewModel = new AddProjectViewModel
      {
        Statuses = statusResult.Result?.Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.StatusName
        }),
        Clients = clientResult.Result?.Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.ClientName
        }),
        Users = userResult.Result?.Select(s => new User
        {
          Id = s.Id,
          FirstName = s.FirstName,
          LastName = s.LastName,
          Email = s.Email,
          Image = s.Image
        })
      },
      EditProjectViewModel = new EditProjectViewModel
      {
        Statuses = statusResult.Result?.Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.StatusName
        }),
        Clients = clientResult.Result?.Select(s => new SelectListItem
        {
          Value = s.Id.ToString(),
          Text = s.ClientName
        }),
        Users = userResult.Result?.Select(s => new User
        {
          Id = s.Id,
          FirstName = s.FirstName,
          LastName = s.LastName,
          Email = s.Email,
          Image = s.Image
        })
      },

      AllCount = allCount,
      StartedCount = startedCount,
      CompletedCount = completedCount
    };

    return View(model);
  }
  #endregion

  #region Post Project Add
  [HttpPost("add")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Add(AddProjectViewModel model)
  {
    if (string.IsNullOrWhiteSpace(model.SelectedUserIds))
    {
      ModelState.AddModelError(nameof(model.SelectedUserIds), "You must select at least one member.");
    }

    if (model.ImageFile != null && model.ImageFile.Length > 0)
    {
      model.Image = await _imageService.UploadAsync(model.ImageFile, "projects");
    }

    var selectedUserIds = model.SelectedUserIds?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(id => id.Trim()).ToList() ?? [];

    if (!ModelState.IsValid)
    {
      await PopulateDropdownsAsync(model);
      model.SelectedUserIds = string.Join(",", selectedUserIds);

      var projects = await _projectService.GetAllProjectsAsync();

      var viewModel = new ProjectsViewModel
      {
        Projects = projects.Result!,
        AddProjectViewModel = model
      };

      ViewData["ShowAddForm"] = "true";

      return View("Projects", viewModel);
    }

    var formData = model.MapTo<AddProjectFormData>();
    formData.UserIds = selectedUserIds;

    var result = await _projectService.CreateProjectAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    await PopulateDropdownsAsync(model);
    model.SelectedUserIds = string.Join(",", selectedUserIds);

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

  #region Get Project Edit
  [HttpGet("edit/{id}")]
  public async Task<IActionResult> Edit(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Projects));
    }

    var projectResult = await _projectService.GetProjectAsync(id);
    if (!projectResult.Succeeded || projectResult.Result == null)
    {
      return RedirectToAction(nameof(Projects));
    }

    var project = projectResult.Result;

    var statuses = await _statusService.GetAllStatusesAsync();
    var clients = await _clientService.GetAllClientsAsync();
    var users = await _userService.GetAllUsersAsync();

    var editModel = new EditProjectViewModel
    {
      Id = project.Id,
      ProjectName = project.ProjectName,
      Description = project.Description,
      StartDate = project.StartDate,
      EndDate = project.EndDate,
      Budget = project.Budget,
      ClientId = project.Client?.Id ?? "",
      Image = project.Image,
      SelectedUserIds = string.Join(",", project.Users?.Select(u => u.Id) ?? []),
      StatusId = project.Status?.Id ?? 0,
      Statuses = statuses.Result?.Select(s => new SelectListItem
      {
        Value = s.Id.ToString(),
        Text = s.StatusName
      }),
      Clients = clients.Result?.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.ClientName
      }),
      Users = users.Result?.Select(u => new User
      {
        Id = u.Id,
        FirstName = u.FirstName,
        LastName = u.LastName,
        Email = u.Email,
        Image = u.Image
      })
    };

    var allProjects = await _projectService.GetAllProjectsAsync();

    var viewModel = new ProjectsViewModel
    {
      Projects = allProjects.Result!,
      EditProjectViewModel = editModel
    };

    ViewData["ShowEditForm"] = "true";

    return View("Projects", viewModel);
  }
  #endregion

  #region Post Project Edit
  [HttpPost("edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(EditProjectViewModel model)
  {
    if (string.IsNullOrWhiteSpace(model.SelectedUserIds))
    {
      ModelState.AddModelError(nameof(model.SelectedUserIds), "You must select at least one member.");
    }

    if (model.ImageFile != null)
    {
      model.Image = await _imageService.UploadAsync(model.ImageFile, "projects");
    }

    var selectedUserIds = model.SelectedUserIds?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(id => id.Trim()).ToList() ?? [];

    if (!ModelState.IsValid)
    {
      await PopulateDropdownsAsync(model);
      model.SelectedUserIds = string.Join(",", selectedUserIds);

      var projects = await _projectService.GetAllProjectsAsync();

      var viewModel = new ProjectsViewModel
      {
        Projects = projects.Result!,
        EditProjectViewModel = model
      };

      ViewData["ShowEditForm"] = "true";

      return View("Projects", viewModel);
    }

    var formData = model.MapTo<EditProjectFormData>();
    formData.UserIds = selectedUserIds;

    var result = await _projectService.UpdateProjectAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    await PopulateDropdownsAsync(model);
    model.SelectedUserIds = string.Join(",", selectedUserIds);

    var fallbackProjects = await _projectService.GetAllProjectsAsync();

    var errorViewModel = new ProjectsViewModel
    {
      Projects = fallbackProjects.Result!,
      EditProjectViewModel = model
    };

    ViewData["ShowEditForm"] = "true";

    return View("Projects", errorViewModel);
  }
  #endregion

  #region Post Project Delete
  [HttpPost("delete/{id}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Projects));
    }

    var result = await _projectService.RemoveProjectAsync(id);
    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Projects));
    }

    var projects = await _projectService.GetAllProjectsAsync();
    var viewModel = new ProjectsViewModel
    {
      Projects = projects.Result!
    };

    return View("Projects", viewModel);
  }
  #endregion

  #region Populate Dropdowns Helper Add
  private async Task PopulateDropdownsAsync(AddProjectViewModel model)
  {
    var fallbackStatuses = await _statusService.GetAllStatusesAsync();
    var fallbackClients = await _clientService.GetAllClientsAsync();
    var fallbackUsers = await _userService.GetAllUsersAsync();

    model.Statuses = fallbackStatuses.Result?.Select(s => new SelectListItem
    {
      Value = s.Id.ToString(),
      Text = s.StatusName
    });

    model.Clients = fallbackClients.Result?.Select(s => new SelectListItem
    {
      Value = s.Id.ToString(),
      Text = s.ClientName
    });

    model.Users = fallbackUsers.Result?.Select(s => new User
    {
      Id = s.Id,
      FirstName = s.FirstName,
      LastName = s.LastName,
      Email = s.Email,
      Image = s.Image
    });
  }
  #endregion
  
  #region Populate Dropdowns Helper Edit
  private async Task PopulateDropdownsAsync(EditProjectViewModel model)
  {
    var fallbackStatuses = await _statusService.GetAllStatusesAsync();
    var fallbackClients = await _clientService.GetAllClientsAsync();
    var fallbackUsers = await _userService.GetAllUsersAsync();

    model.Statuses = fallbackStatuses.Result?.Select(s => new SelectListItem
    {
      Value = s.Id.ToString(),
      Text = s.StatusName
    });

    model.Clients = fallbackClients.Result?.Select(s => new SelectListItem
    {
      Value = s.Id.ToString(),
      Text = s.ClientName
    });

    model.Users = fallbackUsers.Result?.Select(s => new User
    {
      Id = s.Id,
      FirstName = s.FirstName,
      LastName = s.LastName,
      Email = s.Email,
      Image = s.Image
    });
  }
  #endregion
}