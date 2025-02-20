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
			
			Console.WriteLine("-------- PRODUCT MANAGEMENT -------");
			Console.WriteLine("1. Create a Product");
			Console.WriteLine("2. Edit a Product");
			Console.WriteLine("3. Delete a Product");
			Console.WriteLine("4. Show all Products");
			Console.WriteLine("q. Go back");
			Console.WriteLine("-----------------------------------");

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
					Console.WriteLine("Invalid input, try again.");
					Console.ReadKey();
					break;
			}
		}
	}
	
	private async Task CreateProductAsync()
	{
		Console.Clear();
		
		Console.Write("Enter product name: ");
		string name = Console.ReadLine()!;
		
		Console.Write("Enter price: ");
		string price = Console.ReadLine()!;

		if (decimal.TryParse(price, out decimal parsedPrice))
		{
			var productForm = new ProductRegistrationForm { ProductName = name, Price = parsedPrice };
			await _productService.CreateProductAsync(productForm);
		
			Console.WriteLine("Product created successfully!");
		}
		else
		{
			Console.WriteLine("Something went wrong.");
		}
		
		Console.ReadKey();
	}

	private async Task ShowProductsAsync()
	{
		Console.Clear();
		
		var products = await _productService.GetProductsAsync();
		if (products.Any())
		{
			Console.WriteLine("List of all Products:\n");
		
			foreach (var product in products)
			{
				Console.WriteLine($"ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}");
			}
		}
		else
		{
			Console.WriteLine("No Products found.");
		}
		
		Console.WriteLine("\nPress any key to return...");
		Console.ReadKey();
	}

	private async Task EditProductAsync()
	{
		Console.Clear();
		
		Console.Write("Enter Product ID to edit: ");
		if (int.TryParse(Console.ReadLine(), out int id))
		{
			Console.Write("Enter new Product Name: ");
			string newName = Console.ReadLine()!;
			
			Console.Write("Enter new Product Price: ");
			string newPrice = Console.ReadLine()!;

			if (decimal.TryParse(newPrice, out decimal parsedPrice))
			{
				var updateForm = new ProductUpdateForm { Id = id, ProductName = newName, Price = parsedPrice };
				await _productService.UpdateProductAsync(updateForm);
			
				Console.WriteLine("Product updated successfully!");
			}
			else
			{
				Console.WriteLine("Something went wrong.");
			}
		}
		else
		{
			Console.WriteLine("Invalid ID.");
		}
		
		Console.ReadKey();
	}

	private async Task DeleteProductAsync()
	{
		Console.Clear();
		
		Console.Write("Enter Product ID to delete: ");
		if (int.TryParse(Console.ReadLine(), out int id))
		{
			await _productService.DeleteProductAsync(id);
			Console.WriteLine("Product deleted successfully!");
		}
		else
		{
			Console.WriteLine("Invalid ID.");
		}
		
		Console.ReadKey();
	}
}
