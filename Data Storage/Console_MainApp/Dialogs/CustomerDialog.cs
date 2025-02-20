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
			
			Console.WriteLine("-------- CUSTOMER MANAGEMENT -------");
			Console.WriteLine("1. Create a Customer");
			Console.WriteLine("2. Edit a Customer");
			Console.WriteLine("3. Delete a Customer");
			Console.WriteLine("4. Show all Customers");
			Console.WriteLine("q. Go back");
			Console.WriteLine("------------------------------------");

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
					Console.WriteLine("Invalid input, try again.");
					Console.ReadKey();
					break;
			}
		}
	}
	
	private async Task CreateCustomerAsync()
	{
		Console.Clear();
		
		Console.Write("Enter customer name: ");
		string name = Console.ReadLine()!;

		var customerForm = new CustomerRegistrationForm { CustomerName = name };
		await _customerService.CreateCustomerAsync(customerForm);
		
		Console.WriteLine("Customer created successfully!");
		Console.ReadKey();
	}

	private async Task ShowCustomersAsync()
	{
		Console.Clear();
		
		var customers = await _customerService.GetCustomersAsync();
		if (customers.Any())
		{
			Console.WriteLine("List of all Customers:\n");
		
			foreach (var customer in customers)
			{
				Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
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
		
		Console.Write("Enter Customer ID to edit: ");
		if (int.TryParse(Console.ReadLine(), out int id))
		{
			Console.Write("Enter new Customer Name: ");
			string newName = Console.ReadLine()!;

			var updateForm = new CustomerUpdateForm { Id = id, CustomerName = newName };
			await _customerService.UpdateCustomerAsync(updateForm);
			Console.WriteLine("Customer updated successfully!");
		}
		else
		{
			Console.WriteLine("Invalid ID.");
		}
		
		Console.ReadKey();
	}

	private async Task DeleteCustomerAsync()
	{
		Console.Clear();
		
		Console.Write("Enter Customer ID to delete: ");
		if (int.TryParse(Console.ReadLine(), out int id))
		{
			await _customerService.DeleteCustomerAsync(id);
			Console.WriteLine("Customer deleted successfully!");
		}
		else
		{
			Console.WriteLine("Invalid ID.");
		}
		
		Console.ReadKey();
	}
}
