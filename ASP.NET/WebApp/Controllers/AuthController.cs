using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

[Route("")]
public class AuthController : Controller
{
  [HttpGet("login")]
  public IActionResult Login()
  {
    var model = new LoginViewModel();
    return View(model);
  }

  [HttpPost("login")]
  public IActionResult HandleLogin(LoginViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View("Login", model);
    }

    return RedirectToAction("Projects", "Projects");
  }

  [HttpGet("register")]
  public IActionResult Register()
  {
    var model = new RegisterViewModel();
    return View(model);
  }

  [HttpPost("register")]
  public IActionResult HandleRegister(RegisterViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View("Register", model);
    }

    return RedirectToAction("Projects", "Projects");
  }
}