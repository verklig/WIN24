using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class ProjectDialog(ICustomerService customerService, IProductService productService, IProjectService projectService, IStatusTypeService statusTypeService, IUserService userService)
{
	private readonly ICustomerService _customerService = customerService;
	private readonly IProductService _productService = productService;
	private readonly IProjectService _projectService = projectService;
	private readonly IStatusTypeService _statusTypeService = statusTypeService;
	private readonly IUserService _userService = userService;

	public async Task ShowProjectMenuAsync()
	{
		while (true)
		{
			Console.Clear();

			Console.WriteLine("-------- PROJECT MANAGEMENT --------");
			Console.WriteLine("1. Create a Project");
			Console.WriteLine("2. Edit a Project");
			Console.WriteLine("3. Delete a Project");
			Console.WriteLine("4. Show all Projects");
			Console.WriteLine("q. Go back");
			Console.WriteLine("------------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateProjectAsync();
					break;
				case "2":
					await EditProjectAsync();
					break;
				case "3":
					await DeleteProjectAsync();
					break;
				case "4":
					await ShowProjectsAsync();
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

	private async Task CreateProjectAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A PROJECT ----\n");
		
		var statusTypes = await _statusTypeService.GetStatusTypesAsync();
		var customers = await _customerService.GetCustomersAsync();
		var products = await _productService.GetProductsAsync();
		var users = await _userService.GetUsersAsync();

		if (!statusTypes.Any() || !customers.Any() || !products.Any() || !users.Any())
		{
			Console.WriteLine("ERROR: Dependencies not found.");
			Console.WriteLine("Make sure at least one customer, user, product, and status exist before creating a project.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter project title: ");
		string title = Console.ReadLine()!.Trim();
		if (string.IsNullOrEmpty(title))
		{
			Console.WriteLine("\nERROR: The title cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter project description (optional): ");
		string description = Console.ReadLine()!.Trim();

		Console.Write("Enter start date (YYYY-MM-DD): ");
		if (!DateTime.TryParse(Console.ReadLine()!.Trim(), out DateTime startDate))
		{
			Console.WriteLine("\nERROR: Invalid date format.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter end date (optional, YYYY-MM-DD): ");
		DateTime? endDate = null;
		string endDateInput = Console.ReadLine()!.Trim();
		if (!string.IsNullOrEmpty(endDateInput) && DateTime.TryParse(endDateInput, out DateTime parsedEndDate))
		{
			endDate = parsedEndDate;
		}

		Console.WriteLine("\nAvailable statuses:");
		foreach (var status in statusTypes)
		{
			Console.WriteLine($"ID: {status!.Id}, Name: {status.StatusName}");
		}

		Console.Write("\nEnter status ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int statusId))
		{
			Console.WriteLine("\nERROR: Invalid Status ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.WriteLine("\nAvailable customers:");
		foreach (var customer in customers)
		{
			Console.WriteLine($"ID: {customer!.Id}, Name: {customer.CustomerName}");
		}

		Console.Write("\nEnter customer ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int customerId))
		{
			Console.WriteLine("\nERROR: Invalid Customer ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.WriteLine("\nAvailable users:");
		foreach (var user in users)
		{
			Console.WriteLine($"ID: {user!.Id}, Name: {user.FirstName} {user.LastName}");
		}

		Console.Write("\nEnter user ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int userId))
		{
			Console.WriteLine("\nERROR: Invalid User ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.WriteLine("\nAvailable products:");
		foreach (var product in products)
		{
			Console.WriteLine($"ID: {product!.Id}, Name: {product.ProductName}");
		}

		Console.Write("\nEnter product ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int productId))
		{
			Console.WriteLine("\nERROR: Invalid Product ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var projectForm = new ProjectRegistrationForm
		{
			Title = title,
			Description = description,
			StartDate = startDate,
			EndDate = endDate,
			StatusId = statusId,
			CustomerId = customerId,
			ProductId = productId,
			UserId = userId
		};

		bool ok = await _projectService.CreateProjectAsync(projectForm);

		Console.WriteLine(ok ? "\nProject created successfully!" : "\nERROR: Failed to create project.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowProjectsAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL PROJECTS ----\n");
		
		var projects = await _projectService.GetProjectsAsync();
		var statusTypes = await _statusTypeService.GetStatusTypesAsync();
		var customers = await _customerService.GetCustomersAsync();
		var products = await _productService.GetProductsAsync();
		var users = await _userService.GetUsersAsync();

		if (projects.Any())
		{
			foreach (var project in projects)
			{
				var product = products.FirstOrDefault(x => x!.Id == project!.ProductId);
				string statusName = statusTypes.FirstOrDefault(x => x!.Id == project!.StatusId)?.StatusName!;
				string customerName = customers.FirstOrDefault(x => x!.Id == project!.CustomerId)?.CustomerName!;
				string assignedUser = $"{users.First(x => x!.Id == project!.UserId)!.FirstName} {users.First(x => x!.Id == project!.UserId)!.LastName}";

				Console.WriteLine($"ID: {project!.Id}");
				Console.WriteLine($"Title: {project.Title}");
				Console.WriteLine($"Description: {(string.IsNullOrWhiteSpace(project.Description) ? "N/A" : project.Description)}");
				Console.WriteLine($"Starts: {project.StartDate:yyyy-MM-dd}, Ends: {project.EndDate?.ToString("yyyy-MM-dd") ?? "N/A"}");
				Console.WriteLine($"Status: {statusName}");
				Console.WriteLine($"Customer: {customerName}");
				Console.WriteLine($"Project Assigned: {assignedUser}");
				Console.WriteLine($"Service: {product!.ProductName} - Price: {product.Price:C}/h\n");
			}
		}
		else
		{
			Console.WriteLine("No projects found.\n");
		}

		Console.WriteLine("Press any key to return...");
		Console.ReadKey();
	}

	private async Task EditProjectAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A PROJECT ----\n");
		
		var statusTypes = await _statusTypeService.GetStatusTypesAsync();
		var customers = await _customerService.GetCustomersAsync();
		var products = await _productService.GetProductsAsync();
		var users = await _userService.GetUsersAsync();
		
		if (!statusTypes.Any() || !customers.Any() || !products.Any() || !users.Any())
		{
			Console.WriteLine("ERROR: Dependencies not found.");
			Console.WriteLine("Make sure at least one customer, user, product, and status exist before editing a project.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter project ID to edit: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var project = await _projectService.GetProjectByIdAsync(id);
		if (project == null)
		{
			Console.WriteLine("\nERROR: Failed to find project.");
			Console.WriteLine("Make sure a project with the chosen ID exists.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter new project title: ");
		string newTitle = Console.ReadLine()!.Trim();
		if (string.IsNullOrEmpty(newTitle))
		{
			Console.WriteLine("\nERROR: The title cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter new description (optional): ");
		string newDescription = Console.ReadLine()!.Trim();
		
		Console.Write("Enter new start date (YYYY-MM-DD): ");
		if (!DateTime.TryParse(Console.ReadLine()!.Trim(), out DateTime newStartDate))
		{
			Console.WriteLine("\nERROR: Invalid date format.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.Write("Enter new end date (optional, YYYY-MM-DD): ");
		DateTime? newEndDate = null;
		string endDateInput = Console.ReadLine()!.Trim();
		if (!string.IsNullOrEmpty(endDateInput) && DateTime.TryParse(endDateInput, out DateTime parsedEndDate))
		{
			newEndDate = parsedEndDate;
		}
		
		Console.WriteLine("\nAvailable statuses:");
		foreach (var status in statusTypes)
		{
			Console.WriteLine($"ID: {status!.Id}, Name: {status.StatusName}");
		}

		Console.Write("\nEnter new status ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int newStatusId))
		{
			Console.WriteLine("\nERROR: Invalid Status ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.WriteLine("\nAvailable customers:");
		foreach (var customer in customers)
		{
			Console.WriteLine($"ID: {customer!.Id}, Name: {customer.CustomerName}");
		}

		Console.Write("\nEnter new customer ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int newCustomerId))
		{
			Console.WriteLine("\nERROR: Invalid Customer ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.WriteLine("\nAvailable users:");
		foreach (var user in users)
		{
			Console.WriteLine($"ID: {user!.Id}, Name: {user.FirstName} {user.LastName}");
		}

		Console.Write("\nEnter new user ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int newUserId))
		{
			Console.WriteLine("\nERROR: Invalid User ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		Console.WriteLine("\nAvailable products:");
		foreach (var product in products)
		{
			Console.WriteLine($"ID: {product!.Id}, Name: {product.ProductName}");
		}

		Console.Write("\nEnter new product ID: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int newProductId))
		{
			Console.WriteLine("\nERROR: Invalid Product ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		var updateForm = new ProjectUpdateForm
		{
			Id = id,
			Title = newTitle,
			Description = newDescription,
			StartDate = newStartDate,
			EndDate = newEndDate,
			StatusId = newStatusId,
			CustomerId = newCustomerId,
			ProductId = newProductId,
			UserId = newUserId
		};

		bool ok = await _projectService.UpdateProjectAsync(updateForm);
		Console.WriteLine(ok ? "\nProject updated successfully!" : "\nERROR: Failed to update project.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task DeleteProjectAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A PROJECT ----\n");

		Console.Write("Enter project ID to delete: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}

		bool ok = await _projectService.DeleteProjectAsync(id);
		Console.WriteLine(ok ? "\nProject deleted successfully!" : "\nERROR: Failed to delete project.\nMake sure a project with the chosen ID exists.");
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}
