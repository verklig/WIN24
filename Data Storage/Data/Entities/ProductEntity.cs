using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProductEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(150)]
	public string ProductName { get; set; } = null!;
	
	[Required]
	[Column(TypeName = "decimal(18,2)")]
	public decimal Price { get; set; }
}