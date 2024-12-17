using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class ContactService : IContactService
{
	private readonly IFileService _fileService;
	private List<Contact> _contacts = [];

	public bool CreateContact(Contact contact)
	{
		contact.Id = IdGenerator.GenerateId();
		
		_contacts.Add(contact);

		return true;
	}

	public IEnumerable<Contact> GetAllContacts()
	{
		throw new NotImplementedException();
	}
}