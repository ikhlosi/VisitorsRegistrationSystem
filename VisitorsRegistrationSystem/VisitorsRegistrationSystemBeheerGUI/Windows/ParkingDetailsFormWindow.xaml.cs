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
    /// Interaction logic for ParkingDetailsFormWindow.xaml
    /// </summary>
    public partial class ParkingDetailsFormWindow : Window
    {
        ParkingManager _pm;

        public ParkingDetailsFormWindow(ParkingManager pm)
        {
            _pm = pm;
            InitializeComponent();
        }
        public ParkingDetailsFormWindow(ParkingManager pm, ParkingDetailDTO p)
        {
            _pm = pm;
            InitializeComponent();
            InitializeData(p);
        }
        public void InitializeData(ParkingDetailDTO p)
        {
            dtpStartParking.Value = p.StartTime;
            dtpEindParking.Value = p.EndTime;
            txtbBezochtBedrijf.Text = p.VisitedCompanyId.ToString();
            txtbNummerplaat.Text = p.LicensePlate.ToString();
            txtbParking.Text = p.ParkingId.ToString();
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
