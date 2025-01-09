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
			Console.WriteLine("3. Edit a contact");
			Console.WriteLine("4. Delete a contact");
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
				case "3":
					EditContact();
					break;
				case "4":
					DeleteContact();
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

		Console.WriteLine("-------- ADDING CONTACT -------");

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

	private void DeleteContact()
	{
		if (!ShowAllContacts())
		{
			return;
		}

		Console.Write("\nChoose the number of the contact to be deleted: ");
		string choice = Console.ReadLine()!.Trim();

		bool result = _contactService.DeleteContact(choice);

		if (!result)
		{
			Console.WriteLine("\nContact could not be removed.");
			Console.WriteLine("Either the input is wrong or the number doesn't match a contact in the list.");
		}
		else
		{
			Console.WriteLine("\nContact removed!");
		}
	}

	private void EditContact()
	{
		if (!ShowAllContacts())
		{
			return;
		}

		Console.Write("\nChoose the number of the contact to be edited: ");
		string choice = Console.ReadLine()!.Trim();

		if (int.TryParse(choice, out int parseResult))
		{
			IEnumerable<Contact> contacts = _contactService.GetAllContacts().ToList();

			int contactIndex = parseResult - 1;
			Contact contactToEdit = contacts.ElementAt(contactIndex);
			Console.Clear();
			Console.WriteLine($"Editing Contact: {contactToEdit.FirstName} {contactToEdit.LastName}\n");

			contactToEdit.FirstName = PromptAndValidateInput("First Name: ", nameof(contactToEdit.FirstName));
			contactToEdit.LastName = PromptAndValidateInput("Last Name: ", nameof(contactToEdit.LastName));
			contactToEdit.Email = PromptAndValidateInput("Email: ", nameof(contactToEdit.Email));
			contactToEdit.PhoneNumber = PromptAndValidateInput("Phone Number: ", nameof(contactToEdit.PhoneNumber));
			contactToEdit.Street = PromptAndValidateInput("Street: ", nameof(contactToEdit.Street));
			contactToEdit.PostalCode = PromptAndValidateInput("Postal Code: ", nameof(contactToEdit.PostalCode));
			contactToEdit.Locality = PromptAndValidateInput("Town/City: ", nameof(contactToEdit.Locality));

			_contactService.UpdateContact(contactToEdit);
			Console.WriteLine("\nContact updated!");
		}
		else
		{
			Console.WriteLine("\nContact could not be edited.");
			Console.WriteLine("Either the input is wrong or the number doesn't match a contact in the list.");
		}
	}

	private bool ShowAllContacts()
	{
		int count = 1;
		IEnumerable<Contact> contacts = _contactService.GetAllContacts();
		Console.Clear();

		if (contacts.Count() == 0)
		{
			Console.WriteLine("No contacts in list.");
			return false;
		}

		Console.WriteLine("-------- LIST OF CONTACTS -------");
		foreach (Contact contact in contacts)
		{
			Console.WriteLine($"{count}. {contact}");
			count++;
		}

		return true;
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