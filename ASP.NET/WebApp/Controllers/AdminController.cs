using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace Webapp.Controllers;

[Route("admin")]
public class AdminController(IUserService userService) : Controller
{
  private readonly IUserService _userService = userService;

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

  [HttpPost("members/add")]
  public async Task<IActionResult> Add(AddMemberViewModel model)
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

    if (model.ImageFile != null && model.ImageFile.Length > 0)
    {
      var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/members");
      Directory.CreateDirectory(uploadsFolder);

      var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await model.ImageFile.CopyToAsync(stream);
      }

      model.Image = $"/uploads/members/{fileName}";
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

  [HttpPost("members/edit")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(EditMemberViewModel model)
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
      var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/members");
      Directory.CreateDirectory(uploadsFolder);

      var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await model.ImageFile.CopyToAsync(stream);
      }

      model.Image = $"/uploads/members/{fileName}";
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

  [HttpGet("members/edit/{id}")]
  public async Task<IActionResult> Edit(string id)
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

  [HttpPost("members/delete/{id}")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(string id)
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
}