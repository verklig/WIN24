namespace Infrastructure.Models;

public class UserRegistrationForm
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string? Email { get; set; }
	public int RoleId { get; set; }
}