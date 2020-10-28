using DemoProjetGraphiqueAccesDonnees.AccesDonnees;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace DemoProjetGraphiqueAccesDonnees
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDbConnection _connexion;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //https://stackoverflow.com/questions/45796776/get-connectionstring-from-appsettings-json-instead-of-being-hardcoded-in-net-co
            string cheminProjet = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(cheminProjet)
                .AddJsonFile("appsettings.json")
                .Build();

            _connexion = new SqlConnection(configuration.GetConnectionString("tsql2012"));

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Current.MainWindow = new MainWindow(new EmployeDao(_connexion),
                new CommandeDao(_connexion),
                new DetailCommandeDao(_connexion));

            Current.MainWindow.Show();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;

            File.WriteAllText("errors.txt", exception.ToString());

            FermerApplication();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            FermerApplication();
        }

        private void FermerApplication()
        {
            //https://stackoverflow.com/questions/1195829/do-i-have-to-close-a-sqlconnection-before-it-gets-disposed
            _connexion?.Dispose();
        }
    }
}
