using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IContactService
{
	bool CreateContact(Contact contact);
	IEnumerable<Contact> GetAllContacts();
}