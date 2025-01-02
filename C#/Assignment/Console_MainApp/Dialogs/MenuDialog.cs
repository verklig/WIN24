using System.ComponentModel.DataAnnotations;
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

		contact.FirstName = PromptAndValidateInput("First Name: ", nameof(contact.FirstName));
		contact.LastName = PromptAndValidateInput("Last name: ", nameof(contact.LastName));
		contact.Email = PromptAndValidateInput("Email: ", nameof(contact.Email));
		contact.PhoneNumber = PromptAndValidateInput("Phone number: ", nameof(contact.PhoneNumber));
		contact.Street = PromptAndValidateInput("Street: ", nameof(contact.Street));
		contact.PostalCode = PromptAndValidateInput("Postal code: ", nameof(contact.PostalCode));
		contact.Locality = PromptAndValidateInput("Town/city: ", nameof(contact.Locality));

		_contactService.CreateContact(contact);
		Console.WriteLine("\nContact added!");
	}

	private void ShowAllContacts()
	{
		int count = 1;
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
			Console.WriteLine($"{count}. {contact}");
			count++;
		}
	}

	private string PromptAndValidateInput(string prompt, string property)
	{
		while (true)
		{
			Console.Write(prompt);
			string input = Console.ReadLine()!.Trim();
			List<ValidationResult> results = new List<ValidationResult>();
			ValidationContext context = new ValidationContext(ContactFactory.Create()) { MemberName = property };

			if (Validator.TryValidateProperty(input, context, results))
			{
				return input;
			}

			Console.WriteLine($"{results[0].ErrorMessage}, please try again.\n");
		}
	}

	private void DisplayErrorMessage()
	{
		Console.Clear();
		Console.WriteLine("Wrong input, try again.");
	}
}