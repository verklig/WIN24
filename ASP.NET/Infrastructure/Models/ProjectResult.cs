using Domain.Models;

namespace Infrastructure.Models;

public class ProjectResult<T> : BaseResult
{
  public T? Result { get; set; }
}

public class ProjectResult : BaseResult { }