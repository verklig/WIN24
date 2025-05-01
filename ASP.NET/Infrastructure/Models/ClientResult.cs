using Domain.Models;

namespace Infrastructure.Models;

public class ClientResult<T> : BaseResult
{
  public T? Result { get; set; }
}

public class ClientResult : BaseResult { }