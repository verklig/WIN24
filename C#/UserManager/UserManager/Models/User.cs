namespace UserManager.Models;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }

    public User (string name, string email)
    {
        Name = name;
        Email = email;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Created: {Date:yyyy-MM-dd}";
    }
}