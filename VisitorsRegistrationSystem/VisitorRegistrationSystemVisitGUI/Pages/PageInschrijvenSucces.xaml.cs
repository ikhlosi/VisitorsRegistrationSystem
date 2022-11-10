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

namespace VisitorRegistrationSystemVisitGUI.Pages {
    /// <summary>
    /// Interaction logic for PageInschrijvenSucces.xaml
    /// </summary>
    public partial class PageInschrijvenSucces : Page {
        private readonly CompanyManager _cm;
        public PageInschrijvenSucces(CompanyManager cm) {
            _cm = cm;
            InitializeComponent();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Content = new pageMain(_cm);
        }
    }
}
