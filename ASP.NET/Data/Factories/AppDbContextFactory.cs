using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Factories;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

    string connectionString = config.GetConnectionString("DefaultConnection")!;

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    return new AppDbContext(optionsBuilder.Options);
  }
}
