using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class UserEntity : IdentityUser
{
  public string? Image { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? JobTitle { get; set; }
  public string? Address { get; set; }
  public string? DateOfBirth { get; set; }

  public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}