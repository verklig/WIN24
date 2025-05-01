using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddClientViewModel
{
  public IFormFile? ImageFile { get; set; }
  public string? Image { get; set; }

  [Required(ErrorMessage = "You must enter a client name.")]
  [Display(Name = "Client Name", Prompt = "Enter client name")]
  public string ClientName { get; set; } = null!;

  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "You must enter an email address.")]
  [Display(Name = "Email", Prompt = "Enter email address")]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address")]
  public string Email { get; set; } = null!;

  [DataType(DataType.PhoneNumber)]
  [Display(Name = "Phone", Prompt = "Enter phone number")]
  public string? PhoneNumber { get; set; }
}