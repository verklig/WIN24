List<string> itemList = [];

UpdateList();

while (true) 
{
  string userInput = ReadInput();
  string[] part = userInput.Split(' ');

  if (part[0] == "a" || part[0] == "A")
  {
    itemList.Add($"- {part[1]}");
  }
  else if (part[0] == "r" || part[0] == "R")
  {
    itemList.Remove($"- {part[1]}");
  }
  else if (part[0] == "c" || part[0] == "C")
  {
    itemList.Clear();
  }

  UpdateList();
}

void UpdateList()
{
  Console.Clear();
  WriteInfo();  

  foreach (string item in itemList)
  {
    Console.WriteLine(item);
  }
}

void WriteInfo()
{
  Console.WriteLine("Write \"a\" to add or \"r\" to remove before statement to use the list. (ex. a cucumbers)");
  Console.WriteLine();
  Console.WriteLine("-------------------------------");
}

string ReadInput()
{
  Console.WriteLine("-------------------------------");
  Console.WriteLine();
  Console.Write("Add/remove item (\"c\" to clear list): ");
  return Console.ReadLine() ?? "";
}