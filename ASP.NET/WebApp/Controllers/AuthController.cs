using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

[Route("")]
public class AuthController(IAuthService authService) : Controller
{
  private readonly IAuthService _authService = authService;

  [HttpGet("login")]
  public IActionResult Login(string returnUrl = "~/")
  {
    ViewBag.ReturnUrl = returnUrl;
    
    var model = new LoginViewModel();
    return View(model);
  }

  [HttpPost("login")]
  public async Task<IActionResult> HandleLogin(LoginViewModel model, string returnUrl = "~/")
  {
    ViewBag.ErrorMessage = null;
    ViewBag.ReturnUrl = returnUrl;

    if (!ModelState.IsValid)
    {
      return View("Login", model);
    }

    var signUpFormData = model.MapTo<SignInFormData>();
    var result = await _authService.SignInAsync(signUpFormData);
    if (result.Succeeded)
    {
      return LocalRedirect(returnUrl);
    }

    ViewBag.ErrorMessage = result.Error;
    return View(model);
  }

  [HttpGet("register")]
  public IActionResult Register()
  {
      var model = new RegisterViewModel();
      return View(model);
  }

  [HttpPost("register")]
  public async Task<IActionResult> HandleRegister(RegisterViewModel model)
  {
    ViewBag.ErrorMessage = null;

    if (!ModelState.IsValid)
    {
      return View("Register", model);
    }

    var signUpFormData = model.MapTo<SignUpFormData>();
    var result = await _authService.SignUpAsync(signUpFormData);
    if (result.Succeeded)
    {
      return RedirectToAction("Projects", "Projects");
    }

    ViewBag.ErrorMessage = result.Error;
    return View(model);
  }
}