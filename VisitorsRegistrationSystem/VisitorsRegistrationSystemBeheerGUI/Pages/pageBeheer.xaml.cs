using Microsoft.VisualBasic;
using Sprache;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
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
using VisitorsRegistrationSystemBeheerGUI.Windows;
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
        private int ShowEmployeeByCompanyId = 0;

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

        private bool ResultsFilter(object item)
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

                    AddActionButtonsColumn(true);

                    foreach (object item in companies)
                    {
                        dgDataTable.Items.Add(item);
                    }

                    break;
                case "Medewerkers":
                    IReadOnlyList<Employee> employees = _cm.GetEmployees();
                    if (ShowEmployeeByCompanyId > 0) { employees = _cm.GetEmployeesFromCompanyId(ShowEmployeeByCompanyId); }                    

                    cmbSearchParameter.Items.Add("All");
                    foreach (string param in employees[0].GetType().GetProperties().Select(x => x.Name).ToList())
                    {
                        cmbSearchParameter.Items.Add(param);

                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = param;
                        textColumn.Binding = new Binding(param);
                        dgDataTable.Columns.Add(textColumn);
                    }

                    AddActionButtonsColumn(false);

                    foreach (object item in employees)
                    {
                        dgDataTable.Items.Add(item);
                    }

                    break;
                default:
                    break;
            }
            ShowEmployeeByCompanyId = 0;
            cmbSearchParameter.SelectedIndex = 0;
        }

        private void AddActionButtonsColumn(bool showEmployeeButton)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            col.Header = "Actions";
            DataTemplate dt = new DataTemplate();
            var sp = new FrameworkElementFactory(typeof(WrapPanel));
            sp.SetValue(WrapPanel.OrientationProperty, Orientation.Horizontal);
            sp.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);

            if (showEmployeeButton) { sp.AppendChild(AddButton("👤", "EMPLOYEE_ACTION")); }
            sp.AppendChild(AddButton("🖉", "EDIT_ACTION"));
            sp.AppendChild(AddButton("🗑", "DELETE_ACTION"));

            dt.VisualTree = sp;
            col.CellTemplate = dt;
            dgDataTable.Columns.Add(col);
        }

        private FrameworkElementFactory AddButton(string content, string action)
        {
            var btn = new FrameworkElementFactory(typeof(Button));
            btn.SetValue(Button.ContentProperty, content);
            btn.SetValue(Button.StyleProperty, FindResource("ActionButtonTheme") as Style);
            switch (action)
            {
                case "EDIT_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightYellow));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditButton_OnClick));
                    break;
                case "DELETE_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.PaleVioletRed));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteButton_OnClick));
                    break;
                case "EMPLOYEE_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightBlue));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(EmployeeButton_OnClick));
                    break;
                default:
                    break;
            }
            return btn;
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (dgDataTable.SelectedValue.GetType().Name)
            {
                case nameof(Company):
                    BedrijfFormWindow bfw = new BedrijfFormWindow((Company)dgDataTable.SelectedValue);
                    bfw.ShowDialog();
                    break;
                case nameof(Employee):
                    MedewerkerFormWindow mfw = new MedewerkerFormWindow();
                    mfw.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(dgDataTable.SelectedValue.ToString());
        }

        private void EmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowEmployeeByCompanyId = ((Company)dgDataTable.SelectedValue).ID;
            ((RadioButton)stpFilterRadioButtons.Children[1]).IsChecked = true;
        }

        private void cmbSearchParameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtbFilter.Text = "";
            dgDataTable.Items.Filter = ResultsFilter;
        }

        private void txtbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgDataTable.Items.Filter = ResultsFilter;
        }
    }
}
