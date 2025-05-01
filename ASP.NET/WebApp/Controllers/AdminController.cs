using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace Webapp.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin")]
public class AdminController(IUserService userService, IImageService imageService, IClientService clientService) : Controller
{
  private readonly IUserService _userService = userService;
  private readonly IClientService _clientService = clientService;
  private readonly IImageService _imageService = imageService;

  #region Get Members
  [HttpGet("members")]
  public async Task<IActionResult> Members()
  {
    {
      var result = await _userService.GetAllUsersAsync();

      if (!result.Succeeded)
      {
        return View();
      }

      var model = new MembersViewModel
      {
        Users = result.Succeeded && result.Result != null ? result.Result : [],
        AddMemberViewModel = new(),
        EditMemberViewModel = new()
      };

      return View(model);
    }
  }
  #endregion

  #region Post Member Add
  [HttpPost("members/add")]
  public async Task<IActionResult> AddMember(AddMemberViewModel model)
  {
    if (!ModelState.IsValid)
    {
      var users = await _userService.GetAllUsersAsync();

      var viewModel = new MembersViewModel
      {
        Users = users.Result!,
        AddMemberViewModel = model
      };

      ViewData["ShowAddForm"] = "true";

      return View("Members", viewModel);
    }

    if (model.ImageFile != null)
    {
      model.Image = await _imageService.UploadAsync(model.ImageFile, "members");
    }

    var formData = model.MapTo<AddMemberFormData>();

    if (model.BirthDay.HasValue && model.BirthMonth.HasValue && model.BirthYear.HasValue)
    {
      try
      {
        var dob = new DateTime(model.BirthYear.Value, model.BirthMonth.Value, model.BirthDay.Value);
        formData.DateOfBirth = dob.ToString("yyyy-MM-dd");
      }
      catch { }
    }

    var result = await _userService.CreateUserByFormAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Members));
    }

    var fallbackUsers = await _userService.GetAllUsersAsync();

    var errorViewModel = new MembersViewModel
    {
      Users = fallbackUsers.Result!,
      AddMemberViewModel = model
    };

    ViewData["ShowAddForm"] = "true";

    return View("Members", errorViewModel);
  }
  #endregion

  #region Get Member Edit
  [HttpGet("members/edit/{id}")]
  public async Task<IActionResult> EditMember(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Members));
    }

    var userResult = await _userService.GetUserAsync(id);
    if (!userResult.Succeeded || userResult.Result == null)
    {
      return RedirectToAction(nameof(Members));
    }

    var user = userResult.Result;
    var editModel = new EditMemberViewModel
    {
      Id = user.Id,
      FirstName = user.FirstName!,
      LastName = user.LastName!,
      Email = user.Email,
      PhoneNumber = user.PhoneNumber,
      JobTitle = user.JobTitle,
      Address = user.Address,
      Image = user.Image
    };

    if (!string.IsNullOrWhiteSpace(user.DateOfBirth))
    {
      if (DateTime.TryParse(user.DateOfBirth, out var dob))
      {
        editModel.BirthDay = dob.Day;
        editModel.BirthMonth = dob.Month;
        editModel.BirthYear = dob.Year;
      }
    }

    var allUsers = await _userService.GetAllUsersAsync();
    var viewModel = new MembersViewModel
    {
      Users = allUsers.Result!,
      EditMemberViewModel = editModel
    };

    ViewData["ShowEditForm"] = "true";

    return View("Members", viewModel);
  }
  #endregion

  #region Post Member Edit
  [HttpPost("members/edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> EditMember(EditMemberViewModel model)
  {
    if (!ModelState.IsValid)
    {
      var users = await _userService.GetAllUsersAsync();

      var viewModel = new MembersViewModel
      {
        Users = users.Result!,
        EditMemberViewModel = model
      };

      ViewData["ShowEditForm"] = "true";

      return View("Members", viewModel);
    }

    if (model.ImageFile != null && model.ImageFile.Length > 0)
    {
      var user = await _userService.GetUserAsync(model.Id);

      if (!string.IsNullOrWhiteSpace(user.Result?.Image))
      {
        // _imageService.Delete(user.Result.Image);
      }

      model.Image = await _imageService.UploadAsync(model.ImageFile, "members");
    }

    var formData = model.MapTo<EditMemberFormData>();

    if (model.BirthDay.HasValue && model.BirthMonth.HasValue && model.BirthYear.HasValue)
    {
      try
      {
        var dob = new DateTime(model.BirthYear.Value, model.BirthMonth.Value, model.BirthDay.Value);
        formData.DateOfBirth = dob.ToString("yyyy-MM-dd");
      }
      catch { }
    }

    var result = await _userService.UpdateUserAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Members));
    }

    var fallbackUsers = await _userService.GetAllUsersAsync();

    var errorViewModel = new MembersViewModel
    {
      Users = fallbackUsers.Result!,
      EditMemberViewModel = model
    };

    ViewData["ShowEditForm"] = "true";

    return View("Members", errorViewModel);
  }
  #endregion

  #region Post Member Delete
  [HttpPost("members/delete/{id}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteMember(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Members));
    }

    var result = await _userService.RemoveUserAsync(id);
    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Members));
    }

    var users = await _userService.GetAllUsersAsync();
    var viewModel = new MembersViewModel
    {
      Users = users.Result!,
    };

    return View("Members", viewModel);
  }
  #endregion

  #region Get Clients
  [HttpGet("clients")]
  public async Task<IActionResult> Clients()
  {
    {
      var result = await _clientService.GetAllClientsAsync();

      if (!result.Succeeded)
      {
        return View();
      }

      var model = new ClientsViewModel
      {
        Clients = result.Succeeded && result.Result != null ? result.Result : [],
        AddClientViewModel = new(),
        EditClientViewModel = new()
      };

      return View(model);
    }
  }
  #endregion

  #region Post Clients Add
  [HttpPost("clients/add")]
  public async Task<IActionResult> AddClient(AddClientViewModel model)
  {
    if (!ModelState.IsValid)
    {
      var clients = await _clientService.GetAllClientsAsync();

      var viewModel = new ClientsViewModel
      {
        Clients = clients.Result!,
        AddClientViewModel = model
      };

      ViewData["ShowAddForm"] = "true";

      return View("Clients", viewModel);
    }

    if (model.ImageFile != null)
    {
      model.Image = await _imageService.UploadAsync(model.ImageFile, "clients");
    }

    var formData = model.MapTo<AddClientFormData>();
    var result = await _clientService.CreateClientAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Clients));
    }

    var fallbackClients = await _clientService.GetAllClientsAsync();

    var errorViewModel = new ClientsViewModel
    {
      Clients = fallbackClients.Result!,
      AddClientViewModel = model
    };

    ViewData["ShowAddForm"] = "true";

    return View("Clients", errorViewModel);
  }
  #endregion

  #region Get Client Edit
  [HttpGet("clients/edit/{id}")]
  public async Task<IActionResult> EditClient(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Clients));
    }

    var clientResult = await _clientService.GetClientAsync(id);
    if (!clientResult.Succeeded || clientResult.Result == null)
    {
      return RedirectToAction(nameof(Clients));
    }

    var client = clientResult.Result;
    var editModel = new EditClientViewModel
    {
      Id = client.Id,
      ClientName = client.ClientName!,
      Email = client.Email,
      PhoneNumber = client.PhoneNumber,
      Image = client.Image
    };

    var allClients = await _clientService.GetAllClientsAsync();
    var viewModel = new ClientsViewModel
    {
      Clients = allClients.Result!,
      EditClientViewModel = editModel
    };

    ViewData["ShowEditForm"] = "true";

    return View("Clients", viewModel);
  }
  #endregion

  #region Post Client Edit
  [HttpPost("clients/edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> EditClient(EditClientViewModel model)
  {
    if (!ModelState.IsValid)
    {
      var clients = await _clientService.GetAllClientsAsync();

      var viewModel = new ClientsViewModel
      {
        Clients = clients.Result!,
        EditClientViewModel = model
      };

      ViewData["ShowEditForm"] = "true";

      return View("Clients", viewModel);
    }

    if (model.ImageFile != null && model.ImageFile.Length > 0)
    {
      var user = await _userService.GetUserAsync(model.Id);

      if (!string.IsNullOrWhiteSpace(user.Result?.Image))
      {
        // _imageService.Delete(user.Result.Image);
      }

      model.Image = await _imageService.UploadAsync(model.ImageFile, "clients");
    }

    var formData = model.MapTo<EditClientFormData>();
    var result = await _clientService.UpdateClientAsync(formData);

    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Clients));
    }

    var fallbackClients = await _clientService.GetAllClientsAsync();

    var errorViewModel = new ClientsViewModel
    {
      Clients = fallbackClients.Result!,
      EditClientViewModel = model
    };

    ViewData["ShowEditForm"] = "true";

    return View("Clients", errorViewModel);
  }
  #endregion

  #region Post Client Delete
  [HttpPost("clients/delete/{id}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteClient(string id)
  {
    if (string.IsNullOrEmpty(id))
    {
      return RedirectToAction(nameof(Clients));
    }

    var result = await _clientService.RemoveClientAsync(id);
    if (result.Succeeded)
    {
      return RedirectToAction(nameof(Clients));
    }

    var clients = await _clientService.GetAllClientsAsync();
    var viewModel = new ClientsViewModel
    {
      Clients = clients.Result!,
    };

    return View("Clients", viewModel);
  }
  #endregion
}