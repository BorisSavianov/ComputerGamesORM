using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ComputerGamesORM.Data;

public sealed class ComputerGamesContextFactory : IDesignTimeDbContextFactory<ComputerGamesContext>
{
    private const string FallbackConnectionString =
        "Server=(localdb)\\MSSQLLocalDB;Database=ComputerGamesORM;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

    public ComputerGamesContext CreateDbContext(string[] args)
    {
        var configurationBasePath = ResolveConfigurationBasePath();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(configurationBasePath)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? FallbackConnectionString;

        var options = new DbContextOptionsBuilder<ComputerGamesContext>()
            .UseSqlServer(connectionString)
            .EnableDetailedErrors()
            .Options;

        return new ComputerGamesContext(options);
    }

    private static string ResolveConfigurationBasePath()
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (current is not null)
        {
            var winFormsSettingsPath = Path.Combine(
                current.FullName,
                "src",
                "ComputerGamesORM.WinForms",
                "appsettings.json");

            if (File.Exists(winFormsSettingsPath))
            {
                return Path.GetDirectoryName(winFormsSettingsPath) ?? current.FullName;
            }

            var localSettingsPath = Path.Combine(current.FullName, "appsettings.json");
            if (File.Exists(localSettingsPath))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        return Directory.GetCurrentDirectory();
    }
}
