namespace Domain.Models;

public class Client
{
  public string Id { get; set; } = null!;
  public string? Image { get; set; }
  public string ClientName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? PhoneNumber { get; set; }
}