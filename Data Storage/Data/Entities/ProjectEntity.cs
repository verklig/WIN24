using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
	[Key]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(150)]
	public string Title { get; set; } = null!;
	public string? Description { get; set; }
	
	[Required]
	[Column(TypeName = "date")]
	public DateTime StartDate { get; set; }
	
	[Column(TypeName = "date")]
	public DateTime? EndDate { get; set; }
	
	[ForeignKey("Status")]
	public int StatusId { get; set; }
	public StatusTypeEntity Status { get; set; } = null!;
	
	[ForeignKey("Customer")]
	public int CustomerId { get; set; }
	public CustomerEntity Customer { get; set; } = null!;
	
	[ForeignKey("Service")]
	public int ServiceId { get; set; }
	public ServiceEntity Service { get; set; } = null!;
	
	[ForeignKey("User")]
	public int UserId { get; set; }
	public UserEntity User { get; set; } = null!;
}