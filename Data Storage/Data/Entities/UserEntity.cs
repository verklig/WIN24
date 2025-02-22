using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(50)]
	public string FirstName { get; set; } = null!;
	
	[Required]
	[MaxLength(50)]
	public string LastName { get; set; } = null!;
	
	[MaxLength(150)]
	public string? Email { get; set; } = null!;
	
	[ForeignKey("Role")]
	public int RoleId { get; set; }
	public RoleEntity Role { get; set; } = null!;
}