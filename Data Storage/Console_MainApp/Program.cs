using Data.Contexts;
using Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Console_MainApp.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

var config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();

string? connectionString = config.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
	Console.WriteLine("Connection string is missing or not found.");
	Console.WriteLine("\nPress any key to exit...");
	Console.ReadKey();
	return;
}

try
{
	using var connection = new MySqlConnection(connectionString);
	connection.Open();
}
catch (MySqlException ex)
{
	Console.WriteLine("ERROR: Unable to connect to the database.");
	Console.WriteLine($"Details: {ex.Message}");
	Console.WriteLine("\nPress any key to exit...");
	Console.ReadKey();
	return;
}

var services = new ServiceCollection()
	.AddDbContext<DataContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)))
	.AddScoped<CustomerRepository>()
	.AddScoped<ProductRepository>()
	.AddScoped<ProjectRepository>()
	.AddScoped<RoleRepository>()
	.AddScoped<StatusTypeRepository>()
	.AddScoped<UserRepository>()
	.AddScoped<ICustomerService, CustomerService>()
	.AddScoped<IProductService, ProductService>()
	.AddScoped<IProjectService, ProjectService>()
	.AddScoped<IRoleService, RoleService>()
	.AddScoped<IStatusTypeService, StatusTypeService>()
	.AddScoped<IUserService, UserService>()
	.AddScoped<CustomerDialog>()
	.AddScoped<ProductDialog>()
	.AddScoped<ProjectDialog>()
	.AddScoped<RoleDialog>()
	.AddScoped<StatusTypeDialog>()
	.AddScoped<UserDialog>()
	.AddScoped<MainMenuDialog>()
	.BuildServiceProvider();

var mainMenuDialog = services.GetRequiredService<MainMenuDialog>();
await mainMenuDialog.ShowMenuAsync();