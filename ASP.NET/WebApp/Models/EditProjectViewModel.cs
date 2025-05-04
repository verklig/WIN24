using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class EditProjectViewModel
{
  public string Id { get; set; } = null!;
  public IFormFile? ImageFile { get; set; }
  public string? Image { get; set; }

  [Required(ErrorMessage = "You must enter a project name.")]
  [Display(Name = "Project Name", Prompt = "Enter project name")]
  public string ProjectName { get; set; } = null!;

  [Display(Name = "Description", Prompt = "Type something...")]
  public string? Description { get; set; }

  [DataType(DataType.Date)]
  [Required(ErrorMessage = "You must enter a start date.")]
  [Display(Name = "Start Date")]
  public DateTime? StartDate { get; set; } = DateTime.Today;

  [DataType(DataType.Date)]
  [Display(Name = "End Date")]
  public DateTime? EndDate { get; set; }

  [Display(Name = "Budget", Prompt = "0")]
  public decimal? Budget { get; set; }

  [Required(ErrorMessage = "You must select a client.")]
  [Display(Name = "Client", Prompt = "Select a client")]
  public string ClientId { get; set; } = null!;
  public IEnumerable<SelectListItem>? Clients { get; set; }

  [Required(ErrorMessage = "You must select at least one member.")]
  public string SelectedUserIds { get; set; } = null!;
  public IEnumerable<User>? Users { get; set; }

  [Required(ErrorMessage = "You must select a status.")]
  [Display(Name = "Status", Prompt = "Select a status")]
  public int StatusId { get; set; }
  public IEnumerable<SelectListItem>? Statuses { get; set; }
}