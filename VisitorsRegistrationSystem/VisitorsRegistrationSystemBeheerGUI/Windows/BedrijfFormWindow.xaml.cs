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
    /// Interaction logic for BedrijfFormWindow.xaml
    /// </summary>
    public partial class BedrijfFormWindow : Window
    {
        private readonly CompanyManager _cm;
        private Company? _company;

        public BedrijfFormWindow(CompanyManager cm)
        {
            _cm = cm;
            InitializeComponent();
        }

        public BedrijfFormWindow(CompanyManager cm, Company c)
        {
            _cm = cm;
            _company = c;
            InitializeComponent();
            InitializeData(c);
        }

        public void InitializeData(Company c)
        {
            txtbNaam.Text = c.Name;
            txtbVAT.Text = c.VATNumber;
            txtbEmail.Text = c.Email;
            txtbTelNr.Text = c.TelephoneNumber;
            txtbStraat.Text = c.Address.Street;
            txtbHuisnummer.Text = c.Address.HouseNumber;
            txtbBusnummer.Text = c.Address.BusNumber;
            txtbPostcode.Text = c.Address.Postcode;
            txtbGemeente.Text = c.Address.City;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (_company != null)
            {
                _cm.UpdateCompany(CompanyFactory.MakeCompany(_company.ID, txtbNaam.Text, txtbVAT.Text, new Address(_company.Address.Id,txtbGemeente.Text,txtbPostcode.Text, txtbStraat.Text, txtbHuisnummer.Text, txtbBusnummer.Text), txtbTelNr.Text, txtbEmail.Text));
                MessageBox.Show("Bedrijf is Bijgewerkt!");
            }
            else
            {
                _cm.AddCompany(CompanyFactory.MakeCompany(null, txtbNaam.Text, txtbVAT.Text, new Address(txtbGemeente.Text,txtbPostcode.Text, txtbStraat.Text, txtbHuisnummer.Text, txtbBusnummer.Text), txtbTelNr.Text, txtbEmail.Text));
                MessageBox.Show("Bedrijf is Toegevoegd!");
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
