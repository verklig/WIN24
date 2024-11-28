namespace TodoList;
class Todo
{
  public string Description { get; set; }
  public bool IsCompleted { get; set; }

  public Todo(string description, bool isCompleted)
  {
    Description = description;
    IsCompleted = isCompleted;
  }
}