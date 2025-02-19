namespace Infrastructure.Dtos;

public class CustomerUpdateForm
{
	public int Id { get; set; }
	public string CustomerName { get; set; } = null!;
}