using System.Diagnostics;
using System.Text.Json;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
	private readonly IFileService _fileSerivice;

	public ContactRepository(IFileService fileService)
	{
		_fileSerivice = fileService;
	}

	public bool SaveContactsToFile(List<Contact> list)
	{
		try
		{
			string json = JsonSerializer.Serialize(list);
			bool result = _fileSerivice.SaveTextToFile(json);
			return result;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			return false;
		}
	}

	public List<Contact> GetContactsFromFile()
	{
		try
		{
			string json = _fileSerivice.GetTextFromFile();
			if (!string.IsNullOrEmpty(json))
			{
				return JsonSerializer.Deserialize<List<Contact>>(json)!;
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}

		return [];
	}
}