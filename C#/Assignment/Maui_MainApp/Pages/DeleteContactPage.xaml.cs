using Maui_MainApp.ViewModels;
using Infrastructure.Interfaces;

namespace Maui_MainApp.Pages;

public partial class DeleteContactPage : ContentPage
{
	private readonly IContactService _contactService;
	private List<ContactViewModel> _contactViewModels = [];

	public DeleteContactPage(IContactService contactService)
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

	private async void ContactListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is not ContactViewModel selectedContact)
		{
			return;
		}

		var contacts = _contactService.GetAllContacts().ToList();
		var contactToDelete = contacts.FirstOrDefault(c => c.Id == selectedContact.ContactId);

		if (contactToDelete == null)
		{
			await DisplayAlert("Error", "The selected contact could not be found.", "OK");
			return;
		}

		int index = contacts.IndexOf(contactToDelete) + 1;
		string choice = index.ToString();

		bool confirmDelete = await DisplayAlert(
			"Confirm Delete",
			$"Are you sure you want to delete {selectedContact.FullName}?",
			"Yes", "No");

		if (confirmDelete)
		{
			_contactService.DeleteContact(choice);
			LoadContacts();
		}

		ContactListView.SelectedItem = null;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		LoadContacts();
	}
}
