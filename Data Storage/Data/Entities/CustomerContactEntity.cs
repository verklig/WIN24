using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[PrimaryKey(nameof(FirstName), nameof(LastName), nameof(CustomerId))]
public class CustomerContactEntity
{
	[Key]
	[Required]
	[MaxLength(50)]
	public string FirstName { get; set; } = null!;

	[Key]
	[Required]
	[MaxLength(50)]
	public string LastName { get; set; } = null!;

	[ForeignKey("Customer")]
	public int CustomerId { get; set; }
	public CustomerEntity Customer { get; set; } = null!;
}