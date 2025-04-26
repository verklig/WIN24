namespace Infrastructure.Models;

public class StatusResult<T> : BaseResult
{
  public T? Result { get; set; }
}

public class StatusResult : BaseResult { }