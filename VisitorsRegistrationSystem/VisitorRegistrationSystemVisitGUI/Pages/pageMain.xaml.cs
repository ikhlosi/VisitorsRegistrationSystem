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
    /// This class implements a page that is shown when the Visit application is started.
    /// </summary>
    public partial class pageMain : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        /// <summary>
        /// This is the constructor: used to initialize the components and assign the managers.
        /// </summary>
        /// <param name="cm">the CompanyManager that handles the business logic regarding Companies and Employees</param>
        /// <param name="vm">the VisitManager that handles the business logic regarding Visits and Visitors</param>
        public pageMain(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
        }

        /// <summary>
        /// the eventHandler is triggered when the btnInschrijven button is clicked to go to the pageInschrijven page.
        /// </summary>
        /// <param name="sender">the btnInschrijven button when it is clicked</param>
        private void btnInschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageInchrijven(_cm, _vm);
        }

        /// <summary>
        /// the eventHandler is triggered when the btnUitschrijven button is clicked to go to the pageUitschrijven page.
        /// </summary>
        /// <param name="sender">the btnUitschrijven button when it is clicked</param>
        private void btnUitschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageUitschrijven(_cm, _vm);
        }
    }
}
