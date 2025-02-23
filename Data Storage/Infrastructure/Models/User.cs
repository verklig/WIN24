namespace Infrastructure.Models;

public class User
{
	public int Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string? Email { get; set; }
	public Role Role { get; set; } = null!;
}