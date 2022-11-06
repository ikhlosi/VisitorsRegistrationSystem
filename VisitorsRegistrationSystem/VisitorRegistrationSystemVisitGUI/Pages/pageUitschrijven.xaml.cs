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

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageUitschrijven.xaml
    /// </summary>
    public partial class pageUitschrijven : Page
    {
        public pageUitschrijven()
        {
            InitializeComponent();
        }

        private void btnUitschrijven_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain();
        }
    }
}
