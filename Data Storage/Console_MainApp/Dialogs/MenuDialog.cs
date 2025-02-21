namespace Console_MainApp.Dialogs;

public class MenuDialog(CustomerDialog customerDialog, ProductDialog productDialog)
{
	private readonly CustomerDialog _customerDialog = customerDialog;
	private readonly ProductDialog _productDialog = productDialog;
	
	public async Task ShowMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- MAIN MENU --------");
			Console.WriteLine("1. Manage Customers");
			Console.WriteLine("2. Manage Products");
			Console.WriteLine("q. Quit");
			Console.WriteLine("---------------------------");
			
			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();
			
			switch (choice)
			{
				case "1":
					await _customerDialog.ShowCustomerMenuAsync();
					break;
				case "2":
					await _productDialog.ShowProductMenuAsync();
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