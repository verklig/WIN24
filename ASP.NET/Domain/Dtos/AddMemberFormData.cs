namespace Domain.Dtos;

public class AddMemberFormData
{
  public string? Image { get; set; }
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? PhoneNumber { get; set; }
  public string? JobTitle { get; set; }
  public string? Address { get; set; }
  public string? DateOfBirth { get; set; }
}