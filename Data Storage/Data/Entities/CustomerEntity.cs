using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	[MaxLength(50)]
	public string CustomerName { get; set; } = null!;
	public ICollection<CustomerContactEntity> Contacts { get; set; } = new List<CustomerContactEntity>();
}
