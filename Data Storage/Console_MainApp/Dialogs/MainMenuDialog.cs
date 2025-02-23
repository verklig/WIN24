namespace Console_MainApp.Dialogs;

public class MainMenuDialog(CustomerDialog customerDialog, ProductDialog productDialog, ProjectDialog projectDialog, RoleDialog roleDialog, StatusTypeDialog statusTypeDialog, UserDialog userDialog)
{
	private readonly CustomerDialog _customerDialog = customerDialog;
	private readonly ProductDialog _productDialog = productDialog;
	private readonly ProjectDialog _projectDialog = projectDialog;
	private readonly RoleDialog _roleDialog = roleDialog;
	private readonly StatusTypeDialog _statusTypeDialog = statusTypeDialog;
	private readonly UserDialog _userDialog = userDialog;
	
	public async Task ShowMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- MAIN MENU --------");
			Console.WriteLine("1. Manage Projects");
			Console.WriteLine("2. Manage Customers");
			Console.WriteLine("3. Manage Users");
			Console.WriteLine("4. Manage Roles");
			Console.WriteLine("5. Manage Products");
			Console.WriteLine("6. Manage Status Types");
			Console.WriteLine("q. Quit");
			Console.WriteLine("---------------------------");
			
			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();
			
			switch (choice)
			{
				case "1":
					await _projectDialog.ShowProjectMenuAsync();
					break;
				case "2":
					await _customerDialog.ShowCustomerMenuAsync();
					break;
				case "3":
					await _userDialog.ShowUserMenuAsync();
					break;
				case "4":
					await _roleDialog.ShowRoleMenuAsync();
					break;
				case "5":
					await _productDialog.ShowProductMenuAsync();
					break;
				case "6":
					await _statusTypeDialog.ShowStatusTypeMenuAsync();
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
}