using System.ComponentModel.DataAnnotations;
using Infrastructure.Factories;
using Infrastructure.Interfaces;

namespace Maui_MainApp.Pages;

public partial class AddContactPage : ContentPage
{
	private readonly IContactService _contactService;
	
	public AddContactPage(IContactService contactService)
	{
		InitializeComponent();
		_contactService = contactService;
		BindingContext = this;
	}
	
	private async void OnAddContactClicked(object sender, EventArgs e)
	{
		var contact = ContactFactory.Create();

		contact.FirstName = FirstNameEntry.Text;
		contact.LastName = LastNameEntry.Text;
		contact.Email = EmailEntry.Text;
		contact.PhoneNumber = PhoneNumberEntry.Text;
		contact.Street = StreetEntry.Text;
		contact.PostalCode = PostalCodeEntry.Text;
		contact.Locality = LocalityEntry.Text;
		
		var results = new List<ValidationResult>();
		var context = new ValidationContext(contact);

		Validator.TryValidateObject(contact, context, results, true);
		var errors = results.Select(r => r.ErrorMessage).ToList();
		
		if (errors.Any())
		{
			string errorMessage = string.Join("\n", results.Select(r => $"- {r.ErrorMessage}"));
			await DisplayAlert("Error", errorMessage, "OK");
			return;
		}
		
		_contactService.CreateContact(contact);
		
		FirstNameEntry.Text = string.Empty;
		LastNameEntry.Text = string.Empty;
		EmailEntry.Text = string.Empty;
		PhoneNumberEntry.Text = string.Empty;
		StreetEntry.Text = string.Empty;
		PostalCodeEntry.Text = string.Empty;
		LocalityEntry.Text = string.Empty;
		
		await DisplayAlert("Success", "Contact added!", "OK");
	}
}
