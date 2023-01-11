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
    /// This is the main page that will be shown when starting the application.
    /// </summary>
    public partial class pageMain : Page
    {
        private readonly CompanyManager _cm;
        private readonly VisitManager _vm;
        private readonly ParkingManager _pm;

        /// <summary>
        /// This is the constructor: used to assign the managers and set the to be opened tab index depending on the button that is clicked.
        /// </summary>
        /// <param name="cm">the CompanyManager that handles the business logic regarding Companies and Employees</param>
        /// <param name="vm">the VisitManager that handles the business logic regarding Visits and Visitors</param>
        /// <param name="pm">the ParkingManager that handles the business logic regarding Parking, ParkingDetails and ParkingContracts</param>
        public pageMain(CompanyManager cm, VisitManager vm, ParkingManager pm)
        {
            _cm = cm;
            _vm = vm;
            _pm = pm;
            InitializeComponent();
        }

        /// <summary>
        /// This will open the pageBeheer page with Bedrijf as it's selected tab.
        /// </summary>
        /// <param name="sender">the btnBedrijf button when clicked</param>
        private void btnBedrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm,_pm, 0);
        }

        /// <summary>
        /// This will open the pageBeheer page with Werknemers as it's selected tab.
        /// </summary>
        /// <param name="sender">the btnWerknemers button when clicked</param>
        private void btnWerknemers_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 1);
        }

        /// <summary>
        /// This will open the pageBeheer page with Bezoekers as it's selected tab.
        /// </summary>
        /// <param name="sender">the btnBezoekers button when clicked</param>
        private void btnBezoekers_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 2);
        }

        /// <summary>
        /// This will open the pageBeheer page with Bezoeken as it's selected tab.
        /// </summary>
        /// <param name="sender">the btnBezoeken button when clicked</param>
        private void btnBezoeken_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 3);
        }

        /// <summary>
        /// This will open the pageBeheer page with Parking as it's selected tab.
        /// </summary>
        /// <param name="sender">the btnParking button when clicked</param>
        private void btnParking_Click(object sender, RoutedEventArgs e)
        {
            //posible background for button: #A73A17
            Application.Current.MainWindow.Content = new pageBeheer(_cm, _vm, _pm, 4);
        }
    }
}
