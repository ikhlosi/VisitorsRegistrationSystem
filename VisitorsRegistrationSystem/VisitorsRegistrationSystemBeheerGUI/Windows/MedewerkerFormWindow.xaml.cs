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
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for MedewerkerFormWindow.xaml
    /// </summary>
    public partial class MedewerkerFormWindow : Window
    {
        CompanyManager _cm;

        public MedewerkerFormWindow(CompanyManager cm)
        {
            _cm = cm;
            InitializeComponent();

            cmbBedrijf.ItemsSource = _cm.GetCompanies();
        }

        public MedewerkerFormWindow(CompanyManager cm, Employee e)
        {
            _cm = cm;
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
