using Data.Contexts;
using Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Console_MainApp.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
	.AddScoped<MenuDialog>()
	.BuildServiceProvider();

var menuDialog = services.GetRequiredService<MenuDialog>();
await menuDialog.ShowMenuAsync();