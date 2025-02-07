using Data.Entities;

namespace Data.Repositories;

public class UserRepository(DataContext context)
{
	private readonly DataContext _context = context;
}