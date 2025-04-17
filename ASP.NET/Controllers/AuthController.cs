using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

[Route("")]
public class AuthController : Controller
{
  [HttpGet("login")]
  public IActionResult Login()
  {
    return View();
  }

  [HttpPost("login")]
  public IActionResult HandleLogin(LoginViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View("Login");
    }

    return RedirectToAction("Projects", "Projects");
  }

  [HttpGet("register")]
  public IActionResult Register()
  {
    return View();
  }

  [HttpPost("register")]
  public IActionResult HandleRegister(RegisterViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View("Register");
    }

    return RedirectToAction("Projects", "Projects");
  }
}