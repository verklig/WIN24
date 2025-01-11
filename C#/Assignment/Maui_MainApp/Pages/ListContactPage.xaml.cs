using Maui_MainApp.ViewModels;
using Infrastructure.Interfaces;

namespace Maui_MainApp.Pages;

public partial class ListContactPage : ContentPage
{
	private List<ContactViewModel> _contactViewModels = [];

	private readonly IContactService _contactService;

	public ListContactPage(IContactService contactService)
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
	
	protected override void OnAppearing()
	{
		base.OnAppearing();
		LoadContacts();
	}
}
