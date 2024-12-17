namespace Infrastructure.Models;

public class Contact 
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Street { get; set; } = null!;
	public string PostalCode { get; set; } = null!;
	public string Locality { get; set; } = null!;
	public string Id { get; set; } = null!;
}