using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[AllowAnonymous]
[Route("")]
public class AuthController(IAuthService authService) : Controller
{
  private readonly IAuthService _authService = authService;

  [HttpGet("login")]
  public IActionResult Login()
  {
    var model = new LoginViewModel();
    return View(model);
  }

  [HttpPost("login")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> HandleLogin(LoginViewModel model, string returnUrl = "~/")
  {
    ViewBag.ErrorMessage = null;
    ViewBag.ReturnUrl = returnUrl;

    if (!ModelState.IsValid)
    {
      return View("Login", model);
    }

    var signInFormData = model.MapTo<SignInFormData>();
    var result = await _authService.SignInAsync(signInFormData);
    if (result.Succeeded)
    {
      return RedirectToAction("Projects", "Projects");
    }

    ViewBag.ErrorMessage = result.Error;
    return View("Login", model);
  }

  [HttpGet("register")]
  public IActionResult Register()
  {
    var model = new RegisterViewModel();
    return View(model);
  }

  [HttpPost("register")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> HandleRegister(RegisterViewModel model, string returnUrl = "~/")
  {
    ViewBag.ErrorMessage = null;
    ViewBag.ReturnUrl = returnUrl;

    if (!ModelState.IsValid)
    {
      return View("Register", model);
    }

    var signUpFormData = model.MapTo<SignUpFormData>();
    var result = await _authService.SignUpAsync(signUpFormData);
    if (result.Succeeded)
    {
      var signInFormData = new SignInFormData
      {
        Email = model.Email,
        Password = model.Password
      };

      var signInResult = await _authService.SignInAsync(signInFormData);
      if (signInResult.Succeeded)
      {
        return RedirectToAction("Projects", "Projects");
      }

      return RedirectToAction("Login");
    }

    ViewBag.ErrorMessage = result.Error;
    return View("Register", model);
  }

  [HttpPost("logout")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _authService.SignOutAsync();
    return RedirectToAction("Login");
  }
  
  [HttpGet("denied")]
  public IActionResult Denied()
  {
    return RedirectToAction("Projects", "Projects");
  }
}