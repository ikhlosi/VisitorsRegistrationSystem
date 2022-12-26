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
    /// </summary>
    public partial class pageUitschrijven : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        public pageUitschrijven(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }
    }
}
