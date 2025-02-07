using Data.Entities;

namespace Data.Repositories;

public class CustomerRepository(DataContext context)
{
	private readonly DataContext _context = context;
}