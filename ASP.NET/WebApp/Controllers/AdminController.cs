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

      ViewData["ShowForm"] = "true";

      return View("Members", viewModel);
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

    var result = await _userService.CreateUserExtendedAsync(formData);

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

    ViewData["ShowForm"] = "true";

    return View("Members", errorViewModel);
  }
}