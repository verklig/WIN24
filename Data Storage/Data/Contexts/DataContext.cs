using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<CustomerEntity> Customers  { get; set; }
	public DbSet<ServiceEntity> Services  { get; set; }
	public DbSet<StatusTypeEntity> StatusTypes  { get; set; }
	public DbSet<UserEntity> Users  { get; set; }
}