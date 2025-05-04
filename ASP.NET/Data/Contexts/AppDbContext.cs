using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
  public virtual DbSet<ClientEntity> Clients { get; set; }
  public virtual DbSet<ProjectEntity> Projects { get; set; }
  public virtual DbSet<StatusEntity> Status { get; set; }
  public virtual DbSet<UserProjectEntity> UserProjects { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<StatusEntity>().HasData(
      new StatusEntity { Id = 1, StatusName = "Started" },
      new StatusEntity { Id = 2, StatusName = "Completed" }
    );

    modelBuilder.Entity<UserProjectEntity>().HasKey(up => new { up.UserId, up.ProjectId });
    modelBuilder.Entity<UserProjectEntity>().HasOne(up => up.User).WithMany(u => u.UserProjects).HasForeignKey(up => up.UserId);
    modelBuilder.Entity<UserProjectEntity>().HasOne(up => up.Project).WithMany(p => p.ProjectUsers).HasForeignKey(up => up.ProjectId);
  }
}