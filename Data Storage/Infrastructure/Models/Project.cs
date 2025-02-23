namespace Infrastructure.Models;

public class Project
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string? Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public StatusType Status { get; set; } = null!;
	public Customer Customer { get; set; } = null!;
	public Product Product { get; set; } = null!;
	public User User { get; set; } = null!;
}