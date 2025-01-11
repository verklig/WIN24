using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class Contact
{
	[Required(ErrorMessage = "First name is required")]
	[MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
	public string FirstName { get; set; } = null!;

	[Required(ErrorMessage = "Last name is required")]
	[MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
	public string LastName { get; set; } = null!;

	[Required(ErrorMessage = "Email is required")]
	[RegularExpression(@"^[a-öA-Ö0-9._%+-]+@[a-öA-Ö0-9.-]+\.[a-öA-Ö]{2,}$", ErrorMessage = "Email address must be in a valid format (ex. name@domain.com)")]
	public string Email { get; set; } = null!;

	[Required(ErrorMessage = "Phone number is required")]
	[MinLength(7, ErrorMessage = "Phone number must be at least 7 characters long")]
	public string PhoneNumber { get; set; } = null!;

	[Required(ErrorMessage = "Street is required")]
	[MinLength(2, ErrorMessage = "Street must be at least 2 characters long")]
	public string Street { get; set; } = null!;

	[Required(ErrorMessage = "Postal code is required")]
	[MinLength(4, ErrorMessage = "Postal code must be at least 4 characters long")]
	public string PostalCode { get; set; } = null!;

	[Required(ErrorMessage = "Town/city is required")]
	[MinLength(2, ErrorMessage = "Town/city must be at least 2 characters long")]
	public string Locality { get; set; } = null!;

	public string Id { get; set; } = null!;

	public override string ToString()
	{
		return $"Name: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, Street: {Street}, Postal code: {PostalCode}, Town/city: {Locality}, Id: ({Id})";
	}
}