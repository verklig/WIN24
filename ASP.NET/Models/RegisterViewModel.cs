using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public class RegisterViewModel
{
  [Required(ErrorMessage = "You must enter your name.")]
  public string FullName { get; set; } = null!;

  [DataType(DataType.EmailAddress)]
  [Required(ErrorMessage = "You must enter an email address.")]
  [RegularExpression("", ErrorMessage = "You must enter a valid email address")]
  public string Email { get; set; } = null!;

  [DataType(DataType.Password)]
  [Required(ErrorMessage = "You must enter a password.")]
  [RegularExpression("", ErrorMessage = "You must enter a valid password")]
  public string Password { get; set; } = null!;

  [DataType(DataType.Password)]
  [Required(ErrorMessage = "You must confirm the password.")]
  [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
  public string ConfirmPassword { get; set; } = null!;
}