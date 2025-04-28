using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EditMemberViewModel
{
  public string Id { get; set; } = null!;
  public string? Image { get; set; }

  [Required(ErrorMessage = "You must enter first name.")]
  [Display(Name = "First Name", Prompt = "Enter first name")]
  public string FirstName { get; set; } = null!;

  [Required(ErrorMessage = "You must enter last name.")]
  [Display(Name = "Last Name", Prompt = "Enter last name")]
  public string LastName { get; set; } = null!;

  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "You must enter an email address.")]
  [Display(Name = "Email", Prompt = "Enter email address")]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address")]
  public string Email { get; set; } = null!;

  [DataType(DataType.PhoneNumber)]
  [Display(Name = "Phone", Prompt = "Enter phone number")]
  public string? PhoneNumber { get; set; }

  [Display(Name = "Job Title", Prompt = "Enter job title")]
  public string? JobTitle { get; set; }

  [Display(Name = "Address", Prompt = "Enter address")]
  public string? Address { get; set; }

  [Display(Prompt = "Day")]
  public int? BirthDay { get; set; }

  [Display(Prompt = "Month")]
  public int? BirthMonth { get; set; }

  [Display(Prompt = "Year")]
  public int? BirthYear { get; set; }
}