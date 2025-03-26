using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers;

[Route("projects")]
public class ProjectsController : Controller
{
  [Route("")]
  public IActionResult Projects()
  {
    return View();
  }
}