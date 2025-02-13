using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class StatusTypeEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(50)]
	public string StatusName { get; set; } = null!;
}