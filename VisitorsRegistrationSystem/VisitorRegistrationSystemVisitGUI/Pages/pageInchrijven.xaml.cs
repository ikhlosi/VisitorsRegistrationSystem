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
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageInchrijven.xaml
    /// </summary>
    public partial class pageInchrijven : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        public pageInchrijven(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            cbBedrijfAfspraak.ItemsSource = _cm.GetCompanies();
            cbBedrijfAfspraak.SelectedIndex = 0;
        }

        private void btnInschrijven_Click(object sender, RoutedEventArgs e)
        {
            string visitorName = $"{txtbVoornaam.Text} {txtbAchternaam.Text}";
            string visitorEmail = txtbEmail.Text;
            string visitorCompany = txtbBedrijfBezoeker.Text;
            Visitor visitor = VisitorFactory.MakeVisitor(null, visitorName, visitorEmail, visitorCompany);
            Company visitedCompany = (Company)cbBedrijfAfspraak.SelectedItem;
            Employee visitedEmployee = (Employee)cbAfspraakMet.SelectedItem;
            visitor = _vm.AddVisitor(visitor);
            Visit visit = VisitFactory.MakeVisit(null, visitor, visitedCompany, visitedEmployee, DateTime.Now.AddSeconds(1));
            _vm.AddVisit(visit); // todo transacties: we willen geen Visitor toevoegen als we geen Visit hebben kunnen toevoegen
            Application.Current.MainWindow.Content = new PageInschrijvenSucces(_cm, _vm);

            // todo: btnInschrijven enkel klikbaar wanneer alles (correct) ingevuld
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }

        private void cbBedrijfAfspraak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAfspraakMet.ItemsSource = _cm.GetEmployeesFromCompanyId(((Company)cbBedrijfAfspraak.SelectedValue).ID);
            cbAfspraakMet.SelectedIndex = 0;
        }
    }
}
