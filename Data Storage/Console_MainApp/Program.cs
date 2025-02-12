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
	Console.WriteLine("User added successfully!");

	Console.ReadKey();

	var customers = await customerService.GetCustomersAsync();
	foreach (var customer in customers)
	{
			Console.WriteLine($"Customer: {customer.CustomerName}");
	}

	Console.ReadKey();
}
catch (Exception ex)
{
		Console.WriteLine($"An error occurred: {ex.Message}");
		Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
}
finally
{
		Console.ReadKey();
}