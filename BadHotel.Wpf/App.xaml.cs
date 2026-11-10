using System.Windows;
using BadHotel.Core;

namespace BadHotel.Wpf;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IWorldStorage jsonStorage = new JsonWorldStorage();
        IWorldStorage xmlStorage = new XmlWorldStorage();

        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=BadHotelDb;Integrated Security=True;TrustServerCertificate=True;";

        IWorldStorage sqlStorage =
            new SqlWorldStorage(connectionString);

        WorldStorageService storageService =
            new WorldStorageService(
                jsonStorage,
                xmlStorage,
                sqlStorage);

        MainViewModel viewModel =
            new MainViewModel(storageService);

        MainWindow window =
            new MainWindow(viewModel);

        MainWindow = window;
        window.Show();
    }
}
