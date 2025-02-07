using Data.Entities;

namespace Data.Repositories;

public class ServiceRepository(DataContext context)
{
	private readonly DataContext _context = context;
}