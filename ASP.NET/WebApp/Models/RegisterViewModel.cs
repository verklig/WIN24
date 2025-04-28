using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class RegisterViewModel
{
  [Required(ErrorMessage = "You must enter your name.")]
  [Display(Name = "Full Name", Prompt = "Enter your full name")]
  public string FullName { get; set; } = null!;

  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "You must enter an email address.")]
  [Display(Name = "Email", Prompt = "Enter your email address")]
  [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address")]
  public string Email { get; set; } = null!;

  [DataType(DataType.Password)]
  [Required(ErrorMessage = "You must enter a password.")]
  [Display(Name = "Password", Prompt = "Enter your password")]
  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "You must enter a stronger password.")]
  public string Password { get; set; } = null!;

  [DataType(DataType.Password)]
  [Required(ErrorMessage = "You must confirm the password.")]
  [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
  [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
  public string ConfirmPassword { get; set; } = null!;

  [Required(ErrorMessage = "You must accept the terms and conditions.")]
  [Range(typeof(bool), "true", "true")]
  public bool TOS { get; set; }
}