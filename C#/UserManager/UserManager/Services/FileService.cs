using System.Text.Json;
using UserManager.Models;
namespace UserManager.Services;

public class FileService
{
  private readonly string _filePath;

  public FileService(string filePath)
  {
    _filePath = filePath;
  }

  public List<User> LoadUsers()
  {
    if (!File.Exists(_filePath))
    {
      return new List<User>();
    }

    string json = File.ReadAllText(_filePath);
    return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
  }

  public void SaveUsers(List<User> users)
  {
    string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(_filePath, json);
  }
}