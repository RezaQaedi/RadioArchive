using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dna;
using static RadioArchive.DI;
using static Dna.FrameworkDI;

namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            //let the base application do what it needed
            base.OnStartup(e);

            // Set up Dna
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddViewModels()
                .AddWordClientServices()
                .Build();

            // Log it 
            Logger.LogDebugSource("Application starting...");

            //Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            // Set up application view model based on if we are logged in 
            //ViewModelApplication.GoToPage(ApplicationPage.Home);
        }
    }
}
