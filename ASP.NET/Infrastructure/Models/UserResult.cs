using Domain.Models;

namespace Infrastructure.Models;

public class UserResult : BaseResult
{
  public IEnumerable<User>? Result { get; set; }
}