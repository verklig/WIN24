using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
  public IActionResult Index()
  {
    return RedirectToAction("Login", "Auth");
  }
}