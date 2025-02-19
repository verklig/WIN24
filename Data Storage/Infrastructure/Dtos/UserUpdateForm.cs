namespace Infrastructure.Dtos;

public class UserUpdateForm
{
	public int Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string? Email { get; set; }
	public int RoleId { get; set; }
}