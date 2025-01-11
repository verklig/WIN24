namespace Maui_MainApp.ViewModels;

public class ContactViewModel
{
	public string FullName { get; set; } = null!;
	public string ContactDetails { get; set; } = null!;
	public string ContactId { get; set; } = null!;
	public string ContactDetailsWithId => $"{ContactDetails} (Id: {ContactId})";
}