using Domain.Models;

namespace Infrastructure.Models;

public class ClientResult : BaseResult
{
  public IEnumerable<Client>? Result { get; set; }
}