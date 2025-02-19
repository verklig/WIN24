using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class RoleRepository(DataContext context) : BaseRepository<RoleEntity>(context) {}