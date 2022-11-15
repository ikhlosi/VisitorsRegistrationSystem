using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace VisitorsRegistrationSystemBeheerGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageBeheer.xaml
    /// </summary>
    public partial class pageBeheer : Page
    {
        public pageBeheer()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            foreach (RadioButton rb in stpFilterRadioButtons.Children)
            {
                rb.Checked += new RoutedEventHandler(radioButtons_CheckedChanged);
            }

            ((RadioButton)stpFilterRadioButtons.Children[0]).IsChecked = true;
        }


        private void radioButtons_CheckedChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((RadioButton)sender).Content.ToString());

        }
    }
}
