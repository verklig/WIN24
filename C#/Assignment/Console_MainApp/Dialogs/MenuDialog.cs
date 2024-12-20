using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Console_MainApp.Dialogs;

public class MenuDialog
{
	private readonly IContactService _contactService;

	public MenuDialog(IContactService contactService)
	{
		_contactService = contactService;
	}

	public void Menu()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("-------- MAIN MENU -------");
			Console.WriteLine("1. Add a contact");
			Console.WriteLine("2. Show all contacts");
			Console.WriteLine("q. Quit");
			Console.WriteLine("--------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					AddContact();
					break;
				case "2":
					ShowAllContacts();
					break;
				case "q":
					_contactService.SaveContacts();
					return;
				default:
					DisplayErrorMessage();
					break;
			}

			Console.Write("\nPress any button to proceed...");
			Console.ReadKey();
		}
	}

	private void AddContact()
	{
		Contact contact = ContactFactory.Create();
		Console.Clear();

		Console.WriteLine("Adding Contact\n");

		Console.Write("First Name: ");
		contact.FirstName = Console.ReadLine()!.Trim();

		Console.Write("Last Name: ");
		contact.LastName = Console.ReadLine()!.Trim();

		Console.Write("Email: ");
		contact.Email = Console.ReadLine()!.Trim();

		Console.Write("Phone Number: ");
		contact.PhoneNumber = Console.ReadLine()!.Trim();

		Console.Write("Street: ");
		contact.Street = Console.ReadLine()!.Trim();

		Console.Write("Postal Code: ");
		contact.PostalCode = Console.ReadLine()!.Trim();

		Console.Write("Locality: ");
		contact.Locality = Console.ReadLine()!.Trim();

		_contactService.CreateContact(contact);
		Console.WriteLine("\nContact added!");
	}

	private void ShowAllContacts()
	{
		IEnumerable<Contact> contacts = _contactService.GetAllContacts();
		Console.Clear();

		if (contacts.Count() == 0)
		{
			Console.WriteLine("No contacts in list.");
			return;
		}

		Console.WriteLine("List of contacts:\n");
		foreach (Contact contact in contacts)
		{
			Console.WriteLine(contact.ToString());
		}
	}

	private void DisplayErrorMessage()
	{
		Console.Clear();
		Console.WriteLine("Wrong input, try again.");
	}
}