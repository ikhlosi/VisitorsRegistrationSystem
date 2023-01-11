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
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageUitschrijven.xaml
    /// This class implements a page that is shown when the user wants to end their visit.
    /// </summary>
    public partial class pageUitschrijven : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;


        /// <summary>
        /// This is the constructor: used to initialize the components and assign the managers.
        /// </summary>
        /// <param name="cm">the CompanyManager that handles the business logic regarding Companies and Employees</param>
        /// <param name="vm">the VisitManager that handles the business logic regarding Visits and Visitors</param>
        public pageUitschrijven(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
        }

        /// <summary>
        /// The eventHandler is triggered when the btnUitschrijven button is clicked so that the visit endTime can be registered into the system.
        /// </summary>
        /// <param name="sender">the btnUitschrijven button when it is clicked</param>
        private void btnUitschrijven_Click(object sender, RoutedEventArgs e)
        {
            string visitorEmail = txtbEmail.Text;
            DateTime endTime = DateTime.Now;
            try {
                _vm.EndVisit(visitorEmail, endTime);
                Application.Current.MainWindow.Content = new pageUitschrijvenSucces(_cm, _vm);
            }
            catch (Exception ex) {
                var cmb = new CustomMessageBox("Error Bericht", ex.Message);
                cmb.ShowDialog();
                //MessageBox.Show(ve.InnerException.Message);
            }
        }

        /// <summary>
        /// the eventHandler is triggered when the back button Button is clicked to go back to the pageMain page.
        /// </summary>
        /// <param name="sender">the Button button when it is clicked</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }
    }
}
