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
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for ParkingFormWindow.xaml
    /// </summary>
    public partial class ParkingFormWindow : Window
    {
        ParkingManager _pm;

        public ParkingFormWindow(ParkingManager pm)
        {
            _pm = pm;
            InitializeComponent();
        }

        public ParkingFormWindow(ParkingManager pm, ParkingDTO p)
        {
            _pm = pm;
            InitializeComponent();
            InitializeData(p);
        }

        public void InitializeData(ParkingDTO p)
        {
            txtbId.Text = p.ID.ToString();
            txtbAantalPlaatsen.Text = p.totalSpaces.ToString();
            txtbBezettePlaatsen.Text = p.occupiedSpaces.ToString();
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
