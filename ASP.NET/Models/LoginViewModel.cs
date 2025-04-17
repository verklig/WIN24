using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public class LoginViewModel
{
  [Required(ErrorMessage = "You must enter an email.")]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; } = null!;

  [Required(ErrorMessage = "You must enter a password.")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = null!;
}