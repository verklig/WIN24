using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

[Route("")]
public class ProjectsController : Controller
{
  [Route("projects")]
  public IActionResult Projects()
  {
    return View();
  }
}