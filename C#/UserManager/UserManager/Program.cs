using System.Net.Mail;
using UserManager.Models;
using UserManager.Services;

namespace UserManager
{
  class Program
  {
    static void Main(string[] args)
    {
      string filePath = "users.json";
      FileService fileService = new FileService(filePath);
      List<User> users = fileService.LoadUsers();

      while (true)
      {
        Console.Clear();
        Console.WriteLine("-------- MAIN MENU -------");
        Console.WriteLine("1. Add a user");
        Console.WriteLine("2. Show all users");
        Console.WriteLine("q. Quit");
        Console.WriteLine("--------------------------");

        Console.Write("\nChoose an option: ");
        string choice = Console.ReadLine()!.ToLower();

        switch (choice)
        {
          case "1":
            AddUser(users);
            fileService.SaveUsers(users);
            break;
          case "2":
            ShowUsers(users);
            break;
          case "q":
            fileService.SaveUsers(users);
            return;
          default:
            DisplayErrorMessage();
            break;
        }

        Console.Write("\nPress any button to proceed...");
        Console.ReadKey();
      }
    }

    static void AddUser(List<User> users)
    {
      Console.Clear();
      Console.Write("Name: ");
      string name = Console.ReadLine()!.Trim();
      
      Console.Write("Email: ");
      string email = Console.ReadLine()!.Trim();
      Console.Clear();

      if (!String.IsNullOrEmpty(name) && IsValidEmail(email))
      {
        users.Add(new User(name, email));
        Console.WriteLine("User added!");
      }
      else
      {
        Console.WriteLine("User could not be added, name or email is formatted wrong.");
      }
    }

    static void ShowUsers(List<User> users)
    {
      Console.Clear();

      if (users.Count == 0)
      {
        Console.WriteLine("No users in list.");
        return;
      }

      Console.WriteLine("List of users:\n");
      for (int i = 0; i < users.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {users[i]}");
      }
    }

    static bool IsValidEmail(string email)
    {
      try
      {
        new MailAddress(email);
        return true;
      }
      catch (FormatException)
      {
        return false;
      }
    }

    static void DisplayErrorMessage()
    {
      Console.Clear();
      Console.WriteLine("Wrong input, try again.");
    }
  }
}