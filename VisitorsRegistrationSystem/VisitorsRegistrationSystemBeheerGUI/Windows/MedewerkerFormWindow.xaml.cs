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
using System.Windows.Shapes;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for MedewerkerFormWindow.xaml
    /// </summary>
    public partial class MedewerkerFormWindow : Window
    {
        CompanyManager _cm;
        private Employee _employee;

        public MedewerkerFormWindow(CompanyManager cm)
        {
            _cm = cm;
            InitializeComponent();

            cmbBedrijf.ItemsSource = _cm.GetCompanies();
        }

        public MedewerkerFormWindow(CompanyManager cm, Employee e)
        {
            _cm = cm;
            _employee = e;
            InitializeComponent();

            cmbBedrijf.ItemsSource = _cm.GetCompanies();
            InitializeData(e);
        }

        public void InitializeData(Employee e)
        {
            txtbVoornaam.Text = e.Name;
            txtbAchternaam.Text = e.LastName;
            txtbEmail.Text = e.Email;
            txtbFunctie.Text = e.Function;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            Company company = (Company)cmbBedrijf.SelectedItem;
            if (_employee != null)
            {
                _cm.UpdateEmployee(EmployeeFactory.MakeEmployee(_employee.ID, txtbVoornaam.Text, txtbAchternaam.Text, txtbEmail.Text, txtbFunctie.Text, company.ID), company);
                MessageBox.Show("Medewerker is Bijgewerkt!");
            }
            else
            {
                _cm.AddEmployee(EmployeeFactory.MakeEmployee(null, txtbVoornaam.Text, txtbAchternaam.Text, txtbEmail.Text, txtbFunctie.Text, company.ID), company);
                MessageBox.Show("Medewerker is Toegevoegd!");
            }
            this.Close();
        }

        private void btnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
