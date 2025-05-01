namespace Domain.Dtos;

public class AddClientFormData
{
  public string? Image { get; set; }
  public string ClientName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? PhoneNumber { get; set; }
}