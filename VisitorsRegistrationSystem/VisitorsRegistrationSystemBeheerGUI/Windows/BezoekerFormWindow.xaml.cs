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
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for BezoekerFormWindow.xaml
    /// </summary>
    public partial class BezoekerFormWindow : Window
    {
        VisitManager _vm;
        private Visitor? _visitor;

        public BezoekerFormWindow(VisitManager vm)
        {
            _vm = vm;
            InitializeComponent();
        }

        public BezoekerFormWindow(VisitManager vm, Visitor visitor)
        {
            _vm = vm;
            _visitor = visitor;
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
            try
            {
                if (_visitor != null)
                {
                    _vm.UpdateVisitor(VisitorFactory.MakeVisitor(_visitor.Id, txtbNaam.Text, txtbEmail.Text, txtbBedrijf.Text));
                    MessageBox.Show("Bezoeker is Bijgewerkt!");
                }
                else
                {
                    _vm.AddVisitor(VisitorFactory.MakeVisitor(null, txtbNaam.Text, txtbEmail.Text, txtbBedrijf.Text));
                    MessageBox.Show("Bezoeker is Toegevoegd!");
                }
                this.Close();
            } 
            catch (Exception ex)
            {
                if (ex.Message == "VisitManager - UpdateVisitor")
                {
                    this.Close();
                } else { MessageBox.Show("Gelieve alle velden juist in te vullen", "Error"); }
        }
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
