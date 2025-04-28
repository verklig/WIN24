using Domain.Models;

namespace WebApp.Models;

public class MembersViewModel
{
  public IEnumerable<User> Users { get; set; } = [];
  public AddMemberViewModel AddMemberViewModel { get; set; } = new();
  public EditMemberViewModel EditMemberViewModel { get; set; } = new();
}