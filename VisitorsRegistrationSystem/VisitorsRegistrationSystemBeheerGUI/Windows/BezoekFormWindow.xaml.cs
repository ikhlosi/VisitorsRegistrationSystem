﻿using System;
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
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for BezoekFormWindow.xaml
    /// </summary>
    public partial class BezoekFormWindow : Window
    {
        CompanyManager _cm;
        VisitManager _vm;
        private VisitDTO _visit;

        public BezoekFormWindow(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();

            cmbBezoeker.ItemsSource = _vm.GetVisitors();
            cmbBedrijf.ItemsSource = _cm.GetCompanies();
            
        }

        public BezoekFormWindow(CompanyManager cm, VisitManager vm, VisitDTO visit)
        {
            _cm= cm;
            _vm= vm;
            _visit = visit;
            InitializeComponent();

            cmbBezoeker.ItemsSource = _vm.GetVisitors();
            cmbBedrijf.ItemsSource = _cm.GetCompanies();
            InitializeData(visit);
        }

        public void InitializeData(VisitDTO v)
        {
            dtpStartBezoek.Value = v.startTime;
            dtpEindBezoek.Value = v.endTime;
        }

        private void cmbBedrijf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbMedewerker.ItemsSource = _cm.GetEmployeesFromCompanyId(((Company)cmbBedrijf.SelectedItem).ID);
            cmbMedewerker.SelectedIndex = 0;
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            Visitor visitor = (Visitor)cmbBezoeker.SelectedItem;
            Company company = (Company)cmbBedrijf.SelectedItem;
            Employee employee = (Employee)cmbMedewerker.SelectedItem;

            if (_visit != null)
            {
                Visit visit = VisitFactory.MakeVisit(_visit.visitId, visitor, company, employee);
                _vm.UpdateVisit(visit);
                MessageBox.Show("Bezoek is Bijgewerkt!");
            }
            else
            {
                _vm.AddVisit(VisitFactory.MakeVisit(null, visitor, company, employee));
                MessageBox.Show("Bezoek is Toegevoegd!");
            }
            this.Close();
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
