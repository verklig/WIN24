using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class StatusTypeDialog(IStatusTypeService statusTypeService)
{
	private readonly IStatusTypeService _statusTypeService = statusTypeService;

	public async Task ShowStatusTypeMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- STATUS TYPE MANAGEMENT --------");
			Console.WriteLine("1. Create a Status Type");
			Console.WriteLine("2. Edit a Status Type");
			Console.WriteLine("3. Delete a Status Type");
			Console.WriteLine("4. Show all Status Types");
			Console.WriteLine("q. Go back");
			Console.WriteLine("----------------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateStatusTypeAsync();
					break;
				case "2":
					await EditStatusTypeAsync();
					break;
				case "3":
					await DeleteStatusTypeAsync();
					break;
				case "4":
					await ShowStatusTypesAsync();
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
	
	private async Task CreateStatusTypeAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A STATUS TYPE ----\n");
		
		Console.Write("Enter status type name: ");
		string name = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(name))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var statusForm = new StatusTypeRegistrationForm { StatusName = name };
		bool ok = await _statusTypeService.CreateStatusTypeAsync(statusForm);
		
		Console.WriteLine(ok ? "\nStatus type created successfully!" : "\nERROR: Failed to create status type.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowStatusTypesAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL STATUS TYPES ----\n");
		
		var statusTypes = await _statusTypeService.GetStatusTypesAsync();
		if (statusTypes.Any())
		{
			foreach (var status in statusTypes)
			{
				Console.WriteLine($"ID: {status!.Id}, Name: {status.StatusName}");
			}
		}
		else
		{
			Console.WriteLine("No status types found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditStatusTypeAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A STATUS TYPE ----\n");
		
		Console.Write("Enter status type ID to edit: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		var statusType = await _statusTypeService.GetStatusTypeByIdAsync(id);
		if (statusType == null)
		{
			Console.WriteLine("\nERROR: Failed to find status type.");
			Console.WriteLine("Make sure a status type with the chosen ID exists.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new status type name: ");
		string newName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(newName))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var updateForm = new StatusTypeUpdateForm { Id = id, StatusName = newName };
		bool ok = await _statusTypeService.UpdateStatusTypeAsync(updateForm);
		Console.WriteLine(ok ? "\nStatus type updated successfully!" : "\nERROR: Failed to update status type.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
	
	private async Task DeleteStatusTypeAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A STATUS TYPE ----\n");
		
		Console.Write("Enter status type ID to delete: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		bool ok = await _statusTypeService.DeleteStatusTypeAsync(id);
		Console.WriteLine(ok ? "\nStatus type deleted successfully!" : "\nERROR: Failed to delete status type.\nMake sure a status type with the chosen ID exists.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}