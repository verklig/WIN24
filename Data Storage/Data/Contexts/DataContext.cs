using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<CustomerContactEntity> CustomerContacts { get; set; }
	public DbSet<ProjectEntity> Projects { get; set; }
	public DbSet<ServiceEntity> Services { get; set; }
	public DbSet<UserEntity> Users { get; set; }
	public DbSet<RoleEntity> Roles { get; set; }
	public DbSet<StatusTypeEntity> StatusTypes { get; set; }
}