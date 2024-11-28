namespace TodoList;

class Program
{
  public static void Main(string[] args)
  {
    List<Todo> todoList = [];

    while (true)
    {
      Console.Clear();
      Menu();
      string userInput = Console.ReadLine() ?? "";

      switch (userInput)
      {
        case "1":
          AddTodo();
          break;
        case "2":
          ShowTodos();
          break;
        case "q":
          ExitProgram();
          break;
        default:
          ErrorMessage();
          break;
      }
    }

    void Menu()
    {
      Console.WriteLine("1. Add todo item");
      Console.WriteLine("2. Show all todo items");
      Console.WriteLine("q. Exit program\n");
    }

    void AddTodo()
    {
      Console.Clear();
      Console.Write("Add todo item: ");

      string todo = Console.ReadLine()!;

      Console.Clear();
      Console.Write("Is is completed? (y/n): ");

      string isCompleted = Console.ReadLine()!;

      if (!String.IsNullOrWhiteSpace(todo) && isCompleted == "y")
      {
        todoList.Add(new Todo(todo, true));
        Console.Clear();
        Console.Write("Item added!");
        Console.ReadKey();
      }
      else if (!String.IsNullOrWhiteSpace(todo) && isCompleted == "n")
      {
        todoList.Add(new Todo(todo, false));
        Console.Clear();
        Console.Write("Item added!");
        Console.ReadKey();
      }
      else
      {
        Console.Clear();
        Console.Write("Wrong input!");
        Console.ReadKey();
      }
    }

    void ShowTodos()
    {
      Console.Clear();

      foreach (Todo todo in todoList)
      {
        if (todo.IsCompleted)
        {
          Console.WriteLine($"- {todo.Description} (completed)");
        }
        else
        {
          Console.WriteLine($"- {todo.Description}");
        }
      }

      Console.WriteLine();
      Console.ReadKey();
    }

    void ExitProgram()
    {
      Environment.Exit(0);
    }

    void ErrorMessage()
    {
      Console.Clear();
      Console.Write("Input can only be 1, 2 or q\n");
      Console.ReadKey();
    }
  }
}