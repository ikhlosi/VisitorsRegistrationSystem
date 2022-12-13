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
using VisitorsRegistrationSystemBeheerGUI.Model;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for BedrijfFormWindow.xaml
    /// </summary>
    public partial class BedrijfFormWindow : Window
    {
        public BedrijfFormWindow()
        {
            InitializeComponent();
        }

        public BedrijfFormWindow(Company c)
        {
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
            txtbPostcode.Text = "";
            txtbGemeente.Text = c.Address.City;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
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
