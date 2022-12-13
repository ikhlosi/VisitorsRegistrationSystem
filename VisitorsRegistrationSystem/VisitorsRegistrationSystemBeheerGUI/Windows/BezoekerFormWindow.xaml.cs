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
    /// Interaction logic for BezoekerFormWindow.xaml
    /// </summary>
    public partial class BezoekerFormWindow : Window
    {
        VisitManager _vm;

        public BezoekerFormWindow(VisitManager vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        public BezoekerFormWindow(VisitManager vm, Visitor visitor)
        {
            _vm = vm;
            InitializeComponent();
            InitializeData(visitor);
        }

        public void InitializeData(Visitor v)
        {
            txtbNaam.Text = v.Name;
            txtbEmail.Text = v.Email;
            txtbBedrijf.Text = v.VisitorCompany;
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
