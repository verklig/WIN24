using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class RoleDialog(IRoleService roleService)
{
	private readonly IRoleService _roleService = roleService;

	public async Task ShowRoleMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- ROLE MANAGEMENT --------");
			Console.WriteLine("1. Create a Role");
			Console.WriteLine("2. Edit a Role");
			Console.WriteLine("3. Delete a Role");
			Console.WriteLine("4. Show all Roles");
			Console.WriteLine("q. Go back");
			Console.WriteLine("---------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateRoleAsync();
					break;
				case "2":
					await EditRoleAsync();
					break;
				case "3":
					await DeleteRoleAsync();
					break;
				case "4":
					await ShowRolesAsync();
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
	
	private async Task CreateRoleAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A ROLE ----\n");
		
		Console.Write("Enter role name: ");
		string name = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(name))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var roleForm = new RoleRegistrationForm { RoleName = name };
		bool ok = await _roleService.CreateRoleAsync(roleForm);
		
		Console.WriteLine(ok ? "\nRole created successfully!" : "\nERROR: Failed to create role.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowRolesAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL ROLES ----\n");
		
		var roles = await _roleService.GetRolesAsync();
		if (roles.Any())
		{
			foreach (var role in roles)
			{
				Console.WriteLine($"ID: {role!.Id}, Name: {role.RoleName}");
			}
		}
		else
		{
			Console.WriteLine("No roles found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditRoleAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A ROLE ----\n");
		
		Console.Write("Enter role ID to edit: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		var role = await _roleService.GetRoleByIdAsync(id);
		if (role == null)
		{
			Console.WriteLine("\nERROR: Failed to find role.");
			Console.WriteLine("Make sure a role with the chosen ID exists.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new role name: ");
		string newName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(newName))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var updateForm = new RoleUpdateForm { Id = id, RoleName = newName };
		bool ok = await _roleService.UpdateRoleAsync(updateForm);
		Console.WriteLine(ok ? "\nRole updated successfully!" : "\nERROR: Failed to update role.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task DeleteRoleAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A ROLE ----\n");
		
		Console.Write("Enter role ID to delete: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		bool ok = await _roleService.DeleteRoleAsync(id);
		Console.WriteLine(ok ? "\nRole deleted successfully!" : "\nERROR: Failed to delete role.\nMake sure a role with the chosen ID exists.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}
