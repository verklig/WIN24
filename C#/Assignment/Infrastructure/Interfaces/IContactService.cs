using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IContactService
{
	bool CreateContact(Contact contact);
	public bool DeleteContact(string choice);
	public bool SaveContacts();
	IEnumerable<Contact> GetAllContacts();
}