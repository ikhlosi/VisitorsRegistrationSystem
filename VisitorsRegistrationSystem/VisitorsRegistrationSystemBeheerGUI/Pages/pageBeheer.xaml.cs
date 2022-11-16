using Sprache;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageBeheer.xaml
    /// </summary>
    public partial class pageBeheer : Page
    {
        private readonly CompanyManager _cm;

        public pageBeheer(CompanyManager cm)
        {
            _cm = cm;

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

        private bool DestinationFilter(object item)
        {
            if (string.IsNullOrEmpty(txtbFilter.Text) && ((string)cmbSearchParameter.SelectedValue != "All"))
            {
                return true;
            }
            else if((string)cmbSearchParameter.SelectedValue == "All")
            {
                bool result = false;
                foreach (string param in cmbSearchParameter.Items)
                {
                    if (param != "All")
                    {
                        PropertyInfo? pi = item.GetType().GetProperty(param);
                        if (pi.GetValue(item, null).ToString().Contains(txtbFilter.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }
            else
            {
                PropertyInfo? pi = item.GetType().GetProperty(cmbSearchParameter.Text);
                return pi.GetValue(item, null).ToString().Contains(txtbFilter.Text, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void radioButtons_CheckedChanged(object sender, RoutedEventArgs e)
        {
            cmbSearchParameter.Items.Clear();
            dgDataTable.Columns.Clear();
            dgDataTable.Items.Clear();

            switch (((RadioButton)sender).Content.ToString())
            {    
                case "Bedrijven":
                    IReadOnlyList<Company> companies = _cm.GetCompanies();

                    cmbSearchParameter.Items.Add("All");
                    foreach (string param in companies[0].GetType().GetProperties().Select(x => x.Name).ToList())
                    {
                        cmbSearchParameter.Items.Add(param);

                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = param;
                        textColumn.Binding = new Binding(param);
                        dgDataTable.Columns.Add(textColumn);
                    }

                    foreach (object item in companies)
                    {
                        dgDataTable.Items.Add(item);
                    }

                    break;
                case "Medewerkers":
                    IReadOnlyList<Employee> employees = _cm.GetEmployees();

                    cmbSearchParameter.Items.Add("All");
                    foreach (string param in employees[0].GetType().GetProperties().Select(x => x.Name).ToList())
                    {
                        cmbSearchParameter.Items.Add(param);

                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = param;
                        textColumn.Binding = new Binding(param);
                        dgDataTable.Columns.Add(textColumn);
                    }

                    foreach (object item in employees)
                    {
                        dgDataTable.Items.Add(item);
                    }

                    break;
                default:
                    break;
            }
            cmbSearchParameter.SelectedIndex = 0;
        }

        private void cmbSearchParameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtbFilter.Text = "";
            dgDataTable.Items.Filter = DestinationFilter;
        }

        private void txtbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgDataTable.Items.Filter = DestinationFilter;
        }
    }
}
