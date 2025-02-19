using Data.Contexts;
using Data.Repositories;
using Infrastructure.Services;
using Console_MainApp.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Dtos;

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

// TESTING METHODS
var customerService = services.GetRequiredService<CustomerService>();
var projectService = services.GetRequiredService<ProjectService>();

try
{
	// Customer CRUD
	await TestCreateCustomer();
	Console.ReadKey();

	await TestDisplayCustomers();
	Console.ReadKey();

	await TestEditCustomer();
	await TestDisplayCustomers();
	Console.ReadKey();

	await TestDeleteCustomer();
	await TestDisplayCustomers();
	Console.ReadKey();
	
	// Project CRUD
	await TestCreateProject();
	Console.ReadKey();

	await TestDisplayProjects();
	Console.ReadKey();
	
	await TestEditProject();
	await TestDisplayProjects();
	Console.ReadKey();
	
	await TestDeleteProject();
	await TestDisplayProjects();
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
	Console.WriteLine("\nTest completed.");
	Console.ReadKey();
}

async Task TestCreateCustomer()
{
	var testForm = new CustomerRegistrationForm()
	{
		CustomerName = "Test Name",
	};

	await customerService.CreateCustomerAsync(testForm);
	Console.WriteLine("Customer added successfully!");
}

async Task TestDisplayCustomers()
{
	var customers = await customerService.GetCustomersAsync();
	
	Console.WriteLine("\nCurrent Customers:");
	foreach (var customer in customers)
	{
		Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}");
	}
}

async Task TestEditCustomer()
{
	var customers = await customerService.GetCustomersAsync();
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
}

async Task TestDeleteCustomer()
{
	var customers = await customerService.GetCustomersAsync();
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
}

async Task TestCreateProject()
{
	var testForm = new ProjectRegistrationForm()
	{
		Title = "Test Project",
		Description = "This is a test project",
		StartDate = DateTime.Now,
		EndDate = DateTime.Now.AddDays(30),
		StatusId = 1,
		CustomerId = 1,
		ProductId = 1,
		UserId = 1
	};

	await projectService.CreateProjectAsync(testForm);
	Console.WriteLine("Project added successfully!");
}

async Task TestDisplayProjects()
{
	var projects = await projectService.GetProjectsAsync();

	Console.WriteLine("\nCurrent Projects:");
	foreach (var project in projects)
	{
		Console.WriteLine($"Project ID: {project.Id}, Title: {project.Title}, Status ID: {project.StatusId}");
	}
}

async Task TestEditProject()
{
	var projects = await projectService.GetProjectsAsync();
	var projectToUpdate = projects.FirstOrDefault();

	if (projectToUpdate != null)
	{
		var updateForm = new ProjectUpdateForm
		{
			Id = projectToUpdate.Id,
			Title = "Updated Project Title",
			Description = "Updated project description",
			EndDate = DateTime.Now.AddDays(60),
			StatusId = 2
		};

		await projectService.UpdateProjectAsync(updateForm);
		Console.WriteLine($"Project with ID {projectToUpdate.Id} updated successfully!");
	}
	else
	{
		Console.WriteLine("No project found to update.");
	}
}

async Task TestDeleteProject()
{
	var projects = await projectService.GetProjectsAsync();
	var projectToDelete = projects.FirstOrDefault();
	if (projectToDelete != null)
	{
		await projectService.DeleteProjectAsync(projectToDelete.Id);
		Console.WriteLine($"Project with ID {projectToDelete.Id} deleted successfully!");
	}
	else
	{
		Console.WriteLine("No project found to delete.");
	}
}