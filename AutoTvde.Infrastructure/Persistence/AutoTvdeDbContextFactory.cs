using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AutoTvde.Infrastructure.Persistence;

public class AutoTvdeDbContextFactory
    : IDesignTimeDbContextFactory<AutoTvdeDbContext>
{
    public AutoTvdeDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "AutoTvde.Api"
        );

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found."
            );
        }

        var optionsBuilder = new DbContextOptionsBuilder<AutoTvdeDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new AutoTvdeDbContext(optionsBuilder.Options);
    }
}

