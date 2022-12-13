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
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageUitschrijven.xaml
    /// </summary>
    public partial class pageUitschrijven : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        public pageUitschrijven(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
        }

        private void btnUitschrijven_Click(object sender, RoutedEventArgs e)
        {
            string visitorEmail = txtbEmail.Text;
            _vm.DeleteVisit(null);
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
            // todo: end visit  and visitor (visible = 0)
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }
    }
}
