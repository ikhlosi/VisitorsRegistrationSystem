using System;
using System.Collections.Generic;
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

namespace VisitorsRegistrationSystemBeheerGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageMain.xaml
    /// </summary>
    public partial class pageMain : Page
    {
        private readonly CompanyManager _cm;
        private readonly VisitManager _vm;
        private readonly ParkingManager _pm;

        public pageMain(CompanyManager cm, VisitManager vm, ParkingManager pm)
        {
            _cm = cm;
            _vm = vm;
            _pm = pm;
            InitializeComponent();
        }

        private void btnBedrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm,_pm, 0);
        }

        private void btnWerknemers_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 1);
        }

        private void btnBezoekers_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 2);
        }

        private void btnBezoeken_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 3);
        }

        private void btnParking_Click(object sender, RoutedEventArgs e)
        {
            //posible background for button: #A73A17
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 4);
        }
    }
}
