using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;
using VisitorsRegistrationSystemDL.Repositories;

namespace VisitorsRegistrationSystemBeheerGUI
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
            string connString = "Data Source=LAPTOP-GGBV7H48\\SQLEXPRESS;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
            ICompanyRepository companyRepo = new CompanyRepositoryADO(connString);
            CompanyManager companyManager = new CompanyManager(companyRepo);
            MainWindow mw = new MainWindow(companyManager);
            mw.Show();
        }
    }
}
