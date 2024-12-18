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
		try
		{
			contact.Id = IdGenerator.GenerateId();
			
			_contacts.Add(contact);
			
			bool result = _ContactRepository.SaveContactsToFile(_contacts);
			return result;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return false;
		}
	}

	public IEnumerable<Contact> GetAllContacts()
	{
		_contacts = _ContactRepository.GetContactsFromFile();
		return _contacts;
	}
}