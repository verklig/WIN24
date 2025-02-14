using Data.Contexts;
using Data.Repositories;
using Infrastructure.Services;
using Console_MainApp.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Models;

var config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();


string? connectionString = config.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
	Console.WriteLine("Connection string missing!");
	Console.ReadKey();
	return;
}

var services = new ServiceCollection()
	.AddDbContext<DataContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)))
	.AddScoped<CustomerRepository>()
	.AddScoped<ProjectRepository>()
	.AddScoped<CustomerService>()
	.AddScoped<ProjectService>()
	.AddScoped<MenuDialog>()
	.BuildServiceProvider();

// var menuDialog = services.GetRequiredService<MenuDialog>();
// await menuDialog.Menu();

try
{
	var customerService = services.GetRequiredService<CustomerService>();

	var testForm = new CustomerRegistrationForm()
	{
		CustomerName = "Test Name",
	};

	await customerService.CreateCustomerAsync(testForm);
	Console.WriteLine("Customer added successfully!");

	Console.ReadKey();

	var customers = await customerService.GetCustomersAsync();
	
	Console.WriteLine("\nCurrent Customers:");
	foreach (var customer in customers)
	{
		Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}");
	}

	Console.ReadKey();

	var customerToUpdate = customers.FirstOrDefault();
	if (customerToUpdate != null)
	{
		var updateForm = new CustomerUpdateForm
		{
			Id = customerToUpdate.Id,
			CustomerName = "Updated Name"
		};

		await customerService.UpdateCustomerAsync(updateForm);
		Console.WriteLine($"Customer with ID {customerToUpdate.Id} updated successfully!");

		Console.ReadKey();
	}
	else
	{
		Console.WriteLine("No customer found to update.");
	}
	
	customers = await customerService.GetCustomersAsync();
	
	Console.WriteLine("\nCurrent Customers:");
	foreach (var customer in customers)
	{
		Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}");
	}
	
	Console.ReadKey();

	var customerToDelete = customers.FirstOrDefault();
	if (customerToDelete != null)
	{
		await customerService.DeleteCustomerAsync(customerToDelete.Id);
		Console.WriteLine($"Customer with ID {customerToDelete.Id} deleted successfully!");
	}
	else
	{
		Console.WriteLine("No customer found to delete.");
	}

	Console.ReadKey();

	var updatedCustomers = await customerService.GetCustomersAsync();
	
	Console.WriteLine("\nCustomers after updates and deletions:");
	foreach (var customer in updatedCustomers)
	{
		Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}");
	}

	Console.ReadKey();
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred: {ex.Message}");
	if (ex.InnerException != null)
	{
		Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
	}
}
finally
{
	Console.WriteLine("Test completed.");
	Console.ReadKey();
}
