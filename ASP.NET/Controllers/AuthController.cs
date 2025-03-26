using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

public class AuthController : Controller
{
  public IActionResult Login()
  {
    return LocalRedirect("/projects");
    // return View();
  }
}