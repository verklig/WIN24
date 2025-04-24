using Domain.Models;

namespace Infrastructure.Models;

public class StatusResult : BaseResult
{
  public IEnumerable<Status>? Result { get; set; }
}