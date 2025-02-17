namespace Infrastructure.Models;

public class Service
{
	public int Id { get; set; }
	public string ServiceName { get; set; } = null!;
	public decimal Price { get; set; }
}