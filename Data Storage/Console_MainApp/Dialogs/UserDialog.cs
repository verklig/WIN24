using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class UserDialog(IUserService userService, IRoleService roleService)
{
	private readonly IUserService _userService = userService;
	private readonly IRoleService _roleService = roleService;

	public async Task ShowUserMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- USER MANAGEMENT --------");
			Console.WriteLine("1. Create a User");
			Console.WriteLine("2. Edit a User");
			Console.WriteLine("3. Delete a User");
			Console.WriteLine("4. Show all Users");
			Console.WriteLine("q. Go back");
			Console.WriteLine("---------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateUserAsync();
					break;
				case "2":
					await EditUserAsync();
					break;
				case "3":
					await DeleteUserAsync();
					break;
				case "4":
					await ShowUsersAsync();
					break;
				case "q":
					return;
				default:
					Console.WriteLine("\nERROR: Invalid input.");
					Console.WriteLine("\nPress any key to return...");
					Console.ReadKey();
					break;
			}
		}
	}
	
	private async Task CreateUserAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A USER ----\n");
		
		var roles = await _roleService.GetRolesAsync();
		if (!roles.Any())
		{
			Console.WriteLine("ERROR: No roles available.");
			Console.WriteLine("Make sure to create at least one role before creating a user.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter first name: ");
		string firstName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(firstName))
		{
			Console.WriteLine("\nERROR: Either name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter last name: ");
		string lastName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(lastName))
		{
			Console.WriteLine("\nERROR: Either name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter email (optional): ");
		string email = Console.ReadLine()!.Trim();
		
		Console.WriteLine("\nAvailable roles:");
		foreach (var role in roles)
		{
			Console.WriteLine($"ID: {role!.Id}, Name: {role.RoleName}");
		}

		Console.Write("\nEnter role ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int roleId))
		{
			Console.WriteLine("\nERROR: Invalid Role ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var userForm = new UserRegistrationForm { FirstName = firstName, LastName = lastName, Email = email, RoleId = roleId };
		bool ok = await _userService.CreateUserAsync(userForm);
		
		Console.WriteLine(ok ? "\nUser created successfully!" : "\nERROR: Failed to create user.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowUsersAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL USERS ----\n");
		
		var users = await _userService.GetUsersAsync();
		if (users.Any())
		{
			foreach (var user in users)
			{
				string emailDisplay = string.IsNullOrWhiteSpace(user!.Email) ? "N/A" : user.Email;
				
				Console.WriteLine($"ID: {user.Id}, Name: {user.FirstName} {user.LastName}, Email: {emailDisplay}, Role: {user.Role.RoleName}");
			}
		}
		else
		{
			Console.WriteLine("No users found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditUserAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A USER ----\n");
		
		var roles = await _roleService.GetRolesAsync();
		if (!roles.Any())
		{
			Console.WriteLine("ERROR: No roles available.");
			Console.WriteLine("Make sure to create at least one role before editing a user.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter user ID to edit: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		var user = await _userService.GetUserByIdAsync(id);
		if (user == null)
		{
			Console.WriteLine("\nERROR: Failed to find user.");
			Console.WriteLine("Make sure a user with the chosen ID exists.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new first name: ");
		string newFirstName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(newFirstName))
		{
			Console.WriteLine("\nERROR: Either name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new last name: ");
		string newLastName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(newLastName))
		{
			Console.WriteLine("\nERROR: Either name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter new email (optional): ");
		string newEmail = Console.ReadLine()!.Trim();
		
		Console.WriteLine("\nAvailable roles:");
		foreach (var role in roles)
		{
			Console.WriteLine($"ID: {role!.Id}, Name: {role.RoleName}");
		}

		Console.Write("\nEnter new role ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int newRoleId))
		{
			Console.WriteLine("\nERROR: Invalid Role ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var updateForm = new UserUpdateForm { Id = id, FirstName = newFirstName, LastName = newLastName, Email = newEmail, RoleId = newRoleId };
		bool ok = await _userService.UpdateUserAsync(updateForm);
		Console.WriteLine(ok ? "\nUser updated successfully!" : "\nERROR: Failed to update user.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task DeleteUserAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A USER ----\n");
		
		Console.Write("Enter user ID to delete: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		bool ok = await _userService.DeleteUserAsync(id);
		Console.WriteLine(ok ? "\nUser deleted successfully!" : "\nERROR: Failed to delete user.\nMake sure a user with the chosen ID exists.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}