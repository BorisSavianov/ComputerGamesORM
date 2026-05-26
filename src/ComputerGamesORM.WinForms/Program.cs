using ComputerGamesORM.Business;
using ComputerGamesORM.Data;
using ComputerGamesORM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesORM.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.ThreadException += (_, e) => ShowFatalError(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception exception)
            {
                ShowFatalError(exception);
            }
        };

        using var services = BuildServiceProvider();

        try
        {
            services.GetRequiredService<DatabaseInitializer>()
                .InitializeAsync()
                .GetAwaiter()
                .GetResult();

            Application.Run(services.GetRequiredService<MainForm>());
        }
        catch (Exception ex)
        {
            ShowFatalError(ex);
        }
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Server=(localdb)\\MSSQLLocalDB;Database=ComputerGamesORM;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddDbContextFactory<ComputerGamesContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableDetailedErrors();
        });
        services.AddTransient<DatabaseInitializer>();
        services.AddTransient<IGameRepository, GameRepository>();
        services.AddTransient<IGameService, GameService>();
        services.AddTransient<MainForm>();

        return services.BuildServiceProvider();
    }

    private static void ShowFatalError(Exception exception)
    {
        MessageBox.Show(
            exception.Message,
            "ComputerGamesORM",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
