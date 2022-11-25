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

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for BedrijfFormWindow.xaml
    /// </summary>
    public partial class BedrijfFormWindow : Window
    {
        public BedrijfFormWindow(Company c)
        {
            InitializeComponent();
            InitializeData(c);
        }

        public void InitializeData(Company c)
        {
            txtbNaam.Text = c.Name;
            txtbVAT.Text = c.VATNumber;
            txtbEmail.Text = c.Email;
            txtbTelNr.Text = c.TelephoneNumber;
        }
    }
}
