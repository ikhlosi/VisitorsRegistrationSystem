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
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageInchrijven.xaml
    /// </summary>
    public partial class pageInchrijven : Page
    {
        private readonly CompanyManager _cm;

        public pageInchrijven(CompanyManager cm)
        {
            _cm = cm;

            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            cbBedrijfAfspraak.ItemsSource = _cm.GetCompanies();
            cbBedrijfAfspraak.SelectedIndex = 0;

            cbAfspraakMet.ItemsSource = ((Company)cbBedrijfAfspraak.SelectedValue).GetEmployees();
            cbAfspraakMet.SelectedIndex = 0;
        }

        private void btnInschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm);
        }
    }
}
