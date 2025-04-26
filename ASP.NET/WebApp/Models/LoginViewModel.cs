using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class LoginViewModel
{
  [Required(ErrorMessage = "You must enter an email.")]
  [Display(Name = "Email", Prompt = "Enter your email address")]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; } = null!;

  [Required(ErrorMessage = "You must enter a password.")]
  [Display(Name = "Password", Prompt = "Enter your password")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;

  public bool IsPersistent { get; set;}
}