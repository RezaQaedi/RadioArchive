using Dna;
using RadioArchive.Relational;
using System;
using System.Threading;
using System.Windows;
using static Dna.FrameworkDI;

namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex;
        private const string APPGUID = "C53CE3BD-EE1A-4BDF-A85C-BD1C81AA88AA";

        protected async override void OnStartup(StartupEventArgs e)
        {
            mutex = new(true, "Global\\" + APPGUID);

            if (!mutex.WaitOne(TimeSpan.Zero, true))
                Current.Shutdown();

            //let the base application do what it needed
            base.OnStartup(e);

            // Set up Dna
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddClientDataStore()
                .AddViewModels()
                .AddWordClientServices()
                .Build();

            // Log it 
            Logger.LogDebugSource("Application starting...");

            // Ensure of DB
            await DI.ClientDataStore.EnsureDataStoreAsync();

            // storge data 
            DI.StorgeService.Load();

            //Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            // Set up application view model based on if we are logged in 
            //ViewModelApplication.GoToPage(ApplicationPage.Home);
        }
    }
}
