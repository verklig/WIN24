using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EditProjectViewModel
{
  public string Id { get; set; } = null!;
  public string? Image { get; set; }

  [Required(ErrorMessage = "You must enter a project name.")]
  [Display(Name = "Project Name", Prompt = "Enter project name")]
  public string ProjectName { get; set; } = null!;

  [Display(Name = "Description", Prompt = "Type something...")]
  public string? Description { get; set; }

  [DataType(DataType.Date)]
  [Required(ErrorMessage = "You must enter a start date.")]
  [Display(Name = "Start Date")]
  public DateTime StartDate { get; set; }

  [DataType(DataType.Date)]
  [Display(Name = "End Date")]
  public DateTime? EndDate { get; set; }

  [Display(Name = "Budget", Prompt = "0")]
  public decimal? Budget { get; set; }

  [Required(ErrorMessage = "You must choose a client.")]
  [Display(Name = "Client Name", Prompt = "Choose a client")]
  public string ClientId { get; set; } = null!;

  [Required(ErrorMessage = "You must assign a member.")]
  [Display(Name = "Members", Prompt = "Search for members...")]
  public string UserId { get; set; } = null!;

  [Required(ErrorMessage = "You must set a status.")]
  [Display(Name = "Status", Prompt = "Set status")]
  public int StatusId { get; set; }
}