using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IContactRepository
{
  public bool SaveContactsToFile(List<Contact> list);
  public List<Contact> GetContactsFromFile();
}