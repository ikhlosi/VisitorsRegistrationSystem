using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageMain.xaml
    /// </summary>
    public partial class pageMain : Page
    {
        private readonly CompanyManager _cm;

        public pageMain(CompanyManager cm)
        {
            _cm = cm;
            InitializeComponent();
        }

        private void btnInschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageInchrijven(_cm);
        }

        private void btnUitschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageUitschrijven(_cm);
        }
    }
}
