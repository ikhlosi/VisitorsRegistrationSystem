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

namespace VisitorsRegistrationSystemBeheerGUI.Windows
{
    /// <summary>
    /// Interaction logic for ParkingContractenFormWindow.xaml
    /// </summary>
    public partial class ParkingContractenFormWindow : Window
    {
        public ParkingContractenFormWindow()
        {
            InitializeComponent();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet :c");
        }

        private void btnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}