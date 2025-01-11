using System.Diagnostics;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ContactService : IContactService
{
	private readonly IContactRepository _ContactRepository;
	private List<Contact> _contacts = [];

	public ContactService(IContactRepository contactRepository)
	{
		_ContactRepository = contactRepository;
	}

	public bool CreateContact(Contact contact)
	{
		GetAllContacts();
		
		try
		{
			contact.Id = IdGenerator.GenerateId();

			_contacts.Add(contact);

			bool result = SaveContacts();
			return result;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return false;
		}
	}

	public bool DeleteContact(string choice)
	{
		try
		{
			if (!int.TryParse(choice, out int parseResult))
			{
				return false;
			}

			int contactIndex = parseResult - 1;

			_contacts.RemoveAt(contactIndex);

			bool result = SaveContacts();
			return result;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return false;
		}
	}

	public void UpdateContact(Contact contact)
	{
		Contact existingContact = _contacts.FirstOrDefault(c => c.Id == contact.Id)!;

		if (existingContact != null)
		{
			existingContact.FirstName = contact.FirstName;
			existingContact.LastName = contact.LastName;
			existingContact.Email = contact.Email;
			existingContact.PhoneNumber = contact.PhoneNumber;
			existingContact.Street = contact.Street;
			existingContact.PostalCode = contact.PostalCode;
			existingContact.Locality = contact.Locality;

			SaveContacts();
		}
	}

	public bool SaveContacts()
	{
		return _ContactRepository.SaveContactsToFile(_contacts);
	}

	public IEnumerable<Contact> GetAllContacts()
	{
		return _contacts = _ContactRepository.GetContactsFromFile();
	}
}