using Data.Entities;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context)
{
	private readonly DataContext _context = context;
}