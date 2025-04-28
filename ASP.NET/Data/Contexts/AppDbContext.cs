using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
  public virtual DbSet<ClientEntity> Clients { get; set; }
  public virtual DbSet<ProjectEntity> Projects { get; set; }
  public virtual DbSet<StatusEntity> Status { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<StatusEntity>().HasData(
      new StatusEntity { Id = 1, StatusName = "Started" },
      new StatusEntity { Id = 2, StatusName = "Completed" }
    );
  }
}