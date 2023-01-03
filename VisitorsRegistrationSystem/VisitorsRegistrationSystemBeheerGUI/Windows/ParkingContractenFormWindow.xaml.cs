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
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for ParkingContractenFormWindow.xaml
    /// </summary>
    public partial class ParkingContractenFormWindow : Window
    {
        ParkingManager _pm;
        CompanyManager _cm;

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
            MessageBox.Show("Not implemented yet :c");
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
