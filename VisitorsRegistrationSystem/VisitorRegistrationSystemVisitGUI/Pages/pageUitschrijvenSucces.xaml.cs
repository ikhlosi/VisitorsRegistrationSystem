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

namespace VisitorRegistrationSystemVisitGUI.Pages {
    /// <summary>
    /// Interaction logic for pageUitschrijvenSucces.xaml
    /// This class implements a page that is shown when a visitors successfully ends their visit.
    /// </summary>
    public partial class pageUitschrijvenSucces : Page {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        /// <summary>
        /// This is the constructor: used to initialize the components and assign the managers.
        /// </summary>
        /// <param name="cm">the CompanyManager that handles the business logic regarding Companies and Employees</param>
        /// <param name="vm">the VisitManager that handles the business logic regarding Visits and Visitors</param>
        public pageUitschrijvenSucces(CompanyManager cm, VisitManager vm) {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
        }

        /// <summary>
        /// the eventHandler is triggered when the btnHome button is clicked to go back to the pageMain page.
        /// </summary>
        /// <param name="sender">the btnHome button when it is clicked</param>
        private void btnHome_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }

    }
}
