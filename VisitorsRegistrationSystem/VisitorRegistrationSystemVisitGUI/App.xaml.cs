using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;
using VisitorsRegistrationSystemDL.Repositories;

namespace VisitorRegistrationSystemVisitGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(sender.GetType().Name + " " + e.Exception.Message, e.Exception.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DotNetEnv.Env.TraversePath().Load();
            string? connString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
            ICompanyRepository companyRepo = new CompanyRepositoryADO(connString);
            IVisitRepository visitRepo = new VisitRepositoryADO(connString);
            CompanyManager companyManager = new CompanyManager(companyRepo);
            VisitManager visitManager = new VisitManager(visitRepo);
            MainWindow mw = new MainWindow(companyManager, visitManager);
            mw.Show();
        }
    }
}
