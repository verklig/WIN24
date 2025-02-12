using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Factories;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
  public DataContext CreateDbContext(string[] args)
  {
    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

    string connectionString = config.GetConnectionString("DefaultConnection")!;

    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    return new DataContext(optionsBuilder.Options);
  }
}
