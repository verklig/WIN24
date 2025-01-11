using Maui_MainApp.ViewModels;
using Infrastructure.Interfaces;

namespace Maui_MainApp.Pages;

public partial class EditContactPage : ContentPage
{
	private readonly IContactService _contactService;
	private Infrastructure.Models.Contact _selectedContact;
	private List<ContactViewModel> _contactViewModels = [];

	public EditContactPage(IContactService contactService)
	{
		InitializeComponent();
		_contactService = contactService;
		LoadContacts();
		BindingContext = this;
	}

	private void LoadContacts()
	{
		var contacts = _contactService.GetAllContacts().ToList();
		
		if (contacts.Count() == 0)
		{
			NoContactsLabel.IsVisible = true;
			ContactListView.ItemsSource = null;
			return;
		}
		
		NoContactsLabel.IsVisible = false;
		
		_contactViewModels = contacts.Select(contact => new ContactViewModel
		{
			FullName = $"{contact.FirstName} {contact.LastName}",
			ContactDetails = $"Email: {contact.Email}, Phone: {contact.PhoneNumber}, Street: {contact.Street}, Postal code: {contact.PostalCode}, Town/city: {contact.Locality} ",
			ContactId = contact.Id
		}).ToList();

		ContactListView.ItemsSource = _contactViewModels;
	}

	private void ContactListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is not ContactViewModel selectedContactViewModel)
		{
			return;
		}

		var _selectedContactViewModel = selectedContactViewModel;
		_selectedContact = _contactService.GetAllContacts().FirstOrDefault(c => c.Id == selectedContactViewModel.ContactId);

		if (_selectedContact == null)
		{
			DisplayAlert("Error", "Contact not found.", "OK");
			return;
		}

		FirstNameEntry.Text = _selectedContact.FirstName;
		LastNameEntry.Text = _selectedContact.LastName;
		EmailEntry.Text = _selectedContact.Email;
		PhoneNumberEntry.Text = _selectedContact.PhoneNumber;
		StreetEntry.Text = _selectedContact.Street;
		PostalCodeEntry.Text = _selectedContact.PostalCode;
		LocalityEntry.Text = _selectedContact.Locality;

		EditButton.IsEnabled = true;
		ContactListView.SelectedItem = null;
	}

	private void OnEditContactClicked(object sender, EventArgs e)
	{
		_selectedContact.FirstName = FirstNameEntry.Text;
		_selectedContact.LastName = LastNameEntry.Text;
		_selectedContact.Email = EmailEntry.Text;
		_selectedContact.PhoneNumber = PhoneNumberEntry.Text;
		_selectedContact.Street = StreetEntry.Text;
		_selectedContact.PostalCode = PostalCodeEntry.Text;
		_selectedContact.Locality = LocalityEntry.Text;

		_contactService.UpdateContact(_selectedContact);

		DisplayAlert("Success", "Contact updated!", "OK");
		LoadContacts();

		FirstNameEntry.Text = string.Empty;
		LastNameEntry.Text = string.Empty;
		EmailEntry.Text = string.Empty;
		PhoneNumberEntry.Text = string.Empty;
		StreetEntry.Text = string.Empty;
		PostalCodeEntry.Text = string.Empty;
		LocalityEntry.Text = string.Empty;

		EditButton.IsEnabled = false;
	}
	
	protected override void OnAppearing()
	{
		base.OnAppearing();
		LoadContacts();
	}
}
