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
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for ParkingDetailsFormWindow.xaml
    /// </summary>
    public partial class ParkingDetailsFormWindow : Window
    {
        private readonly ParkingManager _pm;
        private readonly CompanyManager _cm;
        private ParkingDetail? _parkingdetail;
        public ParkingDetailsFormWindow(ParkingManager pm, CompanyManager cm)
        {
            _pm = pm;
            _cm = cm;
            InitializeComponent();
            cmbBedrijf.ItemsSource = cm.GetCompanies();
        }
        public ParkingDetailsFormWindow(ParkingManager pm, CompanyManager cm, ParkingDetail p)
        {
            _pm = pm;
            _cm = cm;
            _parkingdetail = ParkingDetailFactory.MakeParkingDetail(p.ID, p.StartTime, p.EndTime, p.LicensePlate, p.VisitedCompany, p.ParkingId);
            InitializeComponent();
            cmbBedrijf.ItemsSource = cm.GetCompanies();
            InitializeData(p);
        }
        public void InitializeData(ParkingDetail p)
        {
            cmbBedrijf.SelectedValuePath = "ID";
            cmbBedrijf.SelectedValue = p.VisitedCompany.ID;
            dtpStartParking.Value = p.StartTime;
            dtpEindParking.Value = p.EndTime;
            txtbNummerplaat.Text = p.LicensePlate.ToString();
            txtbParking.Text = p.ParkingId.ToString();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Company company = (Company)cmbBedrijf.SelectedItem;
                if (_parkingdetail != null)
                {
                    _pm.UpdateParkingDetail(ParkingDetailFactory.MakeParkingDetail(_parkingdetail.ID, (DateTime)dtpStartParking.Value, (DateTime)dtpEindParking.Value, txtbNummerplaat.Text, company, int.Parse(txtbParking.Text)));
                    MessageBox.Show("Parking is Bijgewerkt!");
                }
                else
                {
                    _pm.AddParkingDetail(ParkingDetailFactory.MakeParkingDetail(null, (DateTime)dtpStartParking.Value, (DateTime)dtpEindParking.Value, txtbNummerplaat.Text, company, int.Parse(txtbParking.Text)));
                    MessageBox.Show("Parking is Toegevoegd!");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gelieve alle velden juist in te vullen", "Error");
            }
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
