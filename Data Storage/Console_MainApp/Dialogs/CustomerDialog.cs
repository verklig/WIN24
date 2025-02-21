using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class CustomerDialog(ICustomerService customerService)
{
	private readonly ICustomerService _customerService = customerService;

	public async Task ShowCustomerMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- CUSTOMER MANAGEMENT --------");
			Console.WriteLine("1. Create a Customer");
			Console.WriteLine("2. Edit a Customer");
			Console.WriteLine("3. Delete a Customer");
			Console.WriteLine("4. Show all Customers");
			Console.WriteLine("q. Go back");
			Console.WriteLine("-------------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateCustomerAsync();
					break;
				case "2":
					await EditCustomerAsync();
					break;
				case "3":
					await DeleteCustomerAsync();
					break;
				case "4":
					await ShowCustomersAsync();
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
	
	private async Task CreateCustomerAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A CUSTOMER ----\n");
		
		Console.Write("Enter customer name: ");
		string name = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(name))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			
			return;
		}

		var customerForm = new CustomerRegistrationForm { CustomerName = name };
		bool ok = await _customerService.CreateCustomerAsync(customerForm);
		
		Console.WriteLine(ok ? "\nCustomer created successfully!" : "\nERROR: Failed to create customer.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowCustomersAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL CUSTOMERS ----\n");
		
		var customers = await _customerService.GetCustomersAsync();
		if (customers.Any())
		{
			foreach (var customer in customers)
			{
				Console.WriteLine($"ID: {customer!.Id}, Name: {customer.CustomerName}");
			}
		}
		else
		{
			Console.WriteLine("No Customers found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditCustomerAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A CUSTOMER ----\n");
		
		Console.Write("Enter customer ID to edit: ");
		if (int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.Write("Enter new customer name: ");
			string newName = Console.ReadLine()!.Trim();
			
			if (string.IsNullOrEmpty(newName))
			{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			
			return;
			}

			var updateForm = new CustomerUpdateForm { Id = id, CustomerName = newName };
			bool ok = await _customerService.UpdateCustomerAsync(updateForm);
			
			Console.WriteLine(ok ? "\nCustomer updated successfully!" : "\nERROR: Failed to update customer.\nMake sure a customer with the chosen ID exists.");
		}
		else
		{
			Console.WriteLine("\nERROR: Invalid ID.");		
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task DeleteCustomerAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A CUSTOMER ----\n");
		
		Console.Write("Enter customer ID to delete: ");
		if (int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			bool ok = await _customerService.DeleteCustomerAsync(id);
			Console.WriteLine(ok ? "\nCustomer deleted successfully!" : "\nERROR: Failed to delete customer.\nMake sure a customer with the chosen ID exists.");
		}
		else
		{
			Console.WriteLine("\nERROR: Invalid ID.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}
