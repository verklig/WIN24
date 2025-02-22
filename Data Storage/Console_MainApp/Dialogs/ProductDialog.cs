using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Console_MainApp.Dialogs;

public class ProductDialog(IProductService productService)
{
	private readonly IProductService _productService = productService;

	public async Task ShowProductMenuAsync()
	{
		while (true)
		{
			Console.Clear();
			
			Console.WriteLine("-------- PRODUCT MANAGEMENT --------");
			Console.WriteLine("1. Create a Product");
			Console.WriteLine("2. Edit a Product");
			Console.WriteLine("3. Delete a Product");
			Console.WriteLine("4. Show all Products");
			Console.WriteLine("q. Go back");
			Console.WriteLine("------------------------------------");

			Console.Write("\nChoose an option: ");
			string choice = Console.ReadLine()!.ToLower();

			switch (choice)
			{
				case "1":
					await CreateProductAsync();
					break;
				case "2":
					await EditProductAsync();
					break;
				case "3":
					await DeleteProductAsync();
					break;
				case "4":
					await ShowProductsAsync();
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
	
	private async Task CreateProductAsync()
	{
		Console.Clear();
		Console.WriteLine("---- CREATING A PRODUCT ----\n");
		
		Console.Write("Enter product name: ");
		string name = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(name))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter price (use \",\" instead of \".\" for decimals): ");
		string price = Console.ReadLine()!.Trim();

		if (decimal.TryParse(price, out decimal parsedPrice))
		{
			var productForm = new ProductRegistrationForm { ProductName = name, Price = parsedPrice };
			bool ok = await _productService.CreateProductAsync(productForm);
		
			Console.WriteLine(ok ? "\nProduct created successfully!" : "\nERROR: Failed to create product.");
		}
		else
		{
			Console.WriteLine("\nERROR: Price was formatted wrong.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task ShowProductsAsync()
	{
		Console.Clear();
		Console.WriteLine("---- LIST OF ALL PRODUCTS ----\n");
		
		var products = await _productService.GetProductsAsync();
		if (products.Any())
		{
			foreach (var product in products)
			{
				Console.WriteLine($"ID: {product!.Id}, Name: {product.ProductName}, Price: {product.Price}");
			}
		}
		else
		{
			Console.WriteLine("No products found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditProductAsync()
	{
		Console.Clear();
		Console.WriteLine("---- EDITING A PRODUCT ----\n");
		
		Console.Write("Enter product ID to edit: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		var product = await _productService.GetProductByIdAsync(id);
		if (product == null)
		{
			Console.WriteLine("\nERROR: Failed to find product.");
			Console.WriteLine("Make sure a product with the chosen ID exists.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new product name: ");
		string newName = Console.ReadLine()!.Trim();
		
		if (string.IsNullOrEmpty(newName))
		{
			Console.WriteLine("\nERROR: The name cannot be empty.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		Console.Write("Enter new product price (use \",\" instead of \".\" for decimals): ");
		string newPrice = Console.ReadLine()!.Trim();

		if (decimal.TryParse(newPrice, out decimal parsedPrice))
		{
			var updateForm = new ProductUpdateForm { Id = id, ProductName = newName, Price = parsedPrice };
			bool ok = await _productService.UpdateProductAsync(updateForm);
			Console.WriteLine(ok ? "\nProduct updated successfully!" : "\nERROR: Failed to update product.");
		}
		else
		{
			Console.WriteLine("Price was formatted wrong.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task DeleteProductAsync()
	{
		Console.Clear();
		Console.WriteLine("---- DELETING A PRODUCT ----\n");
		
		Console.Write("Enter product ID to delete: ");
		if (!int.TryParse(Console.ReadLine()!.Trim(), out int id))
		{
			Console.WriteLine("\nERROR: Invalid ID.");
			Console.WriteLine("\nPress any key to return...");
			Console.ReadKey();
			return;
		}
		
		bool ok = await _productService.DeleteProductAsync(id);
		Console.WriteLine(ok ? "\nProduct deleted successfully!" : "\nERROR: Failed to delete product.\nMake sure a product with the chosen ID exists.");
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}
}
