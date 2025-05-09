using WebApp.Models;
using Domain.Dtos;
using Domain.Extensions;
using Data.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Webapp.Controllers;

[AllowAnonymous]
[Route("")]
public class AuthController(IAuthService authService, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : Controller
{
  private readonly IAuthService _authService = authService;
  private readonly SignInManager<UserEntity> _signInManager = signInManager;
  private readonly UserManager<UserEntity> _userManager = userManager;

  #region Get Login Page
  [HttpGet("login")]
  public IActionResult Login()
  {
    var model = new LoginViewModel();
    return View(model);
  }
  #endregion

  #region Post Login
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
  #endregion

  #region Get Register Page
  [HttpGet("register")]
  public IActionResult Register()
  {
    var model = new RegisterViewModel();
    return View(model);
  }
  #endregion

  #region Post Register
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
  #endregion

  #region Post Logout
  [HttpPost("logout")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _authService.SignOutAsync();
    return RedirectToAction("Login");
  }
  #endregion

  #region Get Denied Page
  [HttpGet("denied")]
  public IActionResult Denied()
  {
    return RedirectToAction("Home", "Index");
  }
  #endregion

  #region Post External Login
  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult ExternalLogin(string provider, string returnUrl = null!)
  {
    if (string.IsNullOrEmpty(provider))
    {
      ModelState.AddModelError("", "Invalid provider");
      return View("Login");
    }

    var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { returnUrl });
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return Challenge(properties, provider);
  }
  #endregion

  #region Get External Login
  [HttpGet]
  public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null!, string remoteError = null!)
  {
    returnUrl ??= Url.Content("~/");

    if (!string.IsNullOrEmpty(remoteError))
    {
      ModelState.AddModelError("", $"Error from external provider: {remoteError}");
      return View("Login");
    }

    var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
    if (!result.Succeeded || result?.Principal == null)
    {
      return RedirectToAction("Login");
    }

    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null)
    {
      return RedirectToAction("Login");
    }

    var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
    if (signInResult.Succeeded)
    {
      return RedirectToAction("Projects", "Projects");
    }
    else
    {
      string firstName = string.Empty;
      string lastName = string.Empty;

      try
      {
        firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
        lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
      }
      catch { }

      string email = info.Principal.FindFirstValue(ClaimTypes.Email);
      string username = $"ext_{info.LoginProvider.ToLower()}_{email}";

      var user = new UserEntity { UserName = username, Email = email, FirstName = firstName, LastName = lastName };

      var identityResult = await _userManager.CreateAsync(user);
      if (identityResult.Succeeded)
      {
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, isPersistent: false);

        return LocalRedirect(returnUrl);
      }

      foreach (var error in identityResult.Errors)
      {
        ModelState.AddModelError("", error.Description);
      }

      return View("Login");
    }
  }
  #endregion
}