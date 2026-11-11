
using System.Windows;
using BadHotel.Core;
using BadHotel.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BadHotel.Wpf;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddSingleton(provider =>
            new DbContextOptionsBuilder<HotelDbContext>()
                .UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;Database=BadHotelEfDb;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options);

        // Database storage
        services.AddSingleton<EfWorldStorage>();

        services.AddSingleton<IWorldStorage>(provider =>
            provider.GetRequiredService<EfWorldStorage>());

        // JSON file storage
        services.AddSingleton<JsonWorldStorage>();

        // Application services
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();

        var mainWindow =
            _serviceProvider.GetRequiredService<MainWindow>();

        MainWindow = mainWindow;

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
