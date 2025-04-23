using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

[Route("admin")]
public class AdminController : Controller
{
  [Route("members")]
  public IActionResult Members()
  {
    return View();
  }
}