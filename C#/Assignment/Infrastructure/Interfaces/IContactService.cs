using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IContactService
{
	bool CreateContact(Contact contact);
	public bool SaveContacts();
	IEnumerable<Contact> GetAllContacts();
}