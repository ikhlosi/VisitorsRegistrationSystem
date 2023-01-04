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
    /// Interaction logic for ParkingContractenFormWindow.xaml
    /// </summary>
    public partial class ParkingContractenFormWindow : Window
    {
        private readonly ParkingManager _pm;
        private readonly CompanyManager _cm;
        private ParkingContract? _parkingContract;

        public ParkingContractenFormWindow(ParkingManager pm, CompanyManager cm)
        {
            _pm = pm;
            _cm = cm;
            InitializeComponent();
            cmbBedrijf.ItemsSource = cm.GetCompanies();
        }

        public ParkingContractenFormWindow(ParkingManager pm,CompanyManager cm, ParkingContractDTO p)
        {
            _pm = pm;
            _cm = cm;
            _parkingContract = ParkingContractFactory.MakeParkingContract(p.Id, CompanyFactory.MakeCompany(p.CompanyId, null, null, null, null, null), p.StartDate, p.EndDate, p.Spaces, p.ParkingId);
            InitializeComponent();
            cmbBedrijf.ItemsSource = cm.GetCompanies();
            InitializeData(p);
        }
        public void InitializeData(ParkingContractDTO p)
        {
            txtbParking.Text = p.ParkingId.ToString();
            txtbPlaatsen.Text = p.Spaces.ToString();
            dtpStartContract.Value = p.StartDate;
            dtpEindContract.Value = p.EndDate;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (_parkingContract != null)
                //{
                //    _pm.UpdateParkingContract(ParkingContractFactory.MakeParkingContract(_parkingContract.ID,_parkingContract.Company,p));
                //    MessageBox.Show("Parking is Bijgewerkt!");
                //}
                //else
                //{
                //    _pm.AddParking(ParkingFactory.MakeParking(null, int.Parse(txtbBezettePlaatsen.Text), false, null, null, int.Parse(txtbAantalPlaatsen.Text)));
                //    MessageBox.Show("Parking is Toegevoegd!");
                //}
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
