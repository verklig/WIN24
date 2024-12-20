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

	public override string ToString()
	{
		return $"Name: {FirstName} {LastName} Email: {Email} Phone Number: {PhoneNumber} Street: {Street} Postal Code: {PostalCode} Locality: {Locality} Id: ({Id})";
	}
}