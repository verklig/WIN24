using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ServiceEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(150)]
	public string ServiceName { get; set; } = null!;
	
	[Required]
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }
}