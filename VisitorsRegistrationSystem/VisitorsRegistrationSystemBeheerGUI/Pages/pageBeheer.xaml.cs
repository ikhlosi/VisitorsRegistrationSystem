using Castle.Components.DictionaryAdapter.Xml;
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
using System.Windows.Automation.Provider;
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
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorsRegistrationSystemBeheerGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageBeheer.xaml
    /// </summary>
    public partial class pageBeheer : Page
    {
        private readonly CompanyManager _cm;
        private readonly VisitManager _vm;
        private readonly ParkingManager _pm;

        private int SavedCompanyId = 0;
        private int SavedVisitorId = 0;
        private int SavedParkingId = 0;
        private string? CheckedRadioButton = "";

        private Dictionary<string, string> CompanyParameterDictionary = new Dictionary<string, string> { { "ID", "Id" }, { "Name", "Naam" }, { "VATNumber", "Ondernemingsnummer" }, { "Address", "Adres" }, { "TelephoneNumber", "Telefoonnummer" }, { "Email", "Email" } };
        private Dictionary<string, string> EmployeeParameterDictionary = new Dictionary<string, string> { { "ID", "Id" }, { "Name", "Voornaam" }, { "LastName", "Achternaam" }, { "Email", "Email" }, { "Function", "Functie" }, { "CompanyId", "Bedrijf" } };
        private Dictionary<string, string> VisitorParameterDictionary = new Dictionary<string, string> { { "Id", "Id" }, { "Name", "Naam" }, { "Email", "Email" }, { "VisitorCompany", "Bedrijf" } };
        private Dictionary<string, string> VisitParameterDictionary = new Dictionary<string, string> { { "visitId", "Id" }, { "visitor", "Bezoeker" }, { "company", "Bezochte Bedrijf" }, { "employee", "Bezochte Werknemer" }, { "startTime", "Start Bezoek" }, { "endTime", "Eind Bezoek" } };
        private Dictionary<string, string> ParkingParameterDictionary = new Dictionary<string, string> { { "ID", "Id" }, { "totalSpaces", "Aantal Plaatsen" }, { "occupiedSpaces", "Bezette Plaatsen" } };
        private Dictionary<string, string> ParkingDetailParameterDictionary = new Dictionary<string, string> { { "Id", "Id" }, { "StartTime", "Start Parking" }, { "EndTime", "Einde Parking" }, { "LicensePlate", "Nummerplaat" }, { "VisitedCompanyId", "Bezocht bedrijf" }, { "ParkingId", "Parking" } };
        private Dictionary<string, string> ParkingContractParameterDictionary = new Dictionary<string, string> { { "Id", "Id" }, { "CompanyId", "Bedrijf" }, { "Spaces", "Gereserveerde Plaatsen" }, { "StartDate", "Start Datum" }, { "EndDate", "Eind Datum" }, { "ParkingId", "Parking" } };

        public pageBeheer(CompanyManager cm, VisitManager vm,ParkingManager pm, int tabIndex)
        {
            _cm = cm;
            _vm = vm;
            _pm = pm;
            
            InitializeComponent();
            InitializeData(tabIndex);
        }

        public void InitializeData(int i)
        {
            foreach (RadioButton rb in stpFilterRadioButtons.Children)
            {
                rb.Checked += new RoutedEventHandler(radioButtons_CheckedChanged);
            }

            ((RadioButton)stpFilterRadioButtons.Children[i]).IsChecked = true;
        }

        private Dictionary<string,string> GetParamaterDictionary(object item)
        {
            switch (item.GetType().Name)
            {
                case nameof(Company):
                    return CompanyParameterDictionary;
                case nameof(Employee):
                    return EmployeeParameterDictionary;
                case nameof(Visitor):
                    return VisitorParameterDictionary;
                case nameof(VisitDTO):
                    return VisitParameterDictionary;
                case nameof(ParkingDTO):
                    return ParkingParameterDictionary;
                case nameof(ParkingDetailDTO):
                    return ParkingDetailParameterDictionary;
                case nameof(ParkingContractDTO):
                    return ParkingContractParameterDictionary;
                default:
                    return null;
            }
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
                foreach (string param in cmbSearchParameter.Items )
                {
                    if (param != "All")
                    {
                        Dictionary<string,string> parameterDictionary = GetParamaterDictionary(item);

                        var key = parameterDictionary.FirstOrDefault(x => x.Value == param).Key;
                        PropertyInfo? pi = item.GetType().GetProperty(key);
                        if (pi.GetValue(item, null) != null)
                        {
                            if (pi.GetValue(item, null).ToString().Contains(txtbFilter.Text, StringComparison.OrdinalIgnoreCase))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                return result;
            }
            else
            {
                Dictionary<string, string> parameterDictionary = GetParamaterDictionary(item);
                if (parameterDictionary == null) return true;

                var key = parameterDictionary.FirstOrDefault(x => x.Value == cmbSearchParameter.SelectedValue.ToString()).Key;
                PropertyInfo? pi = item.GetType().GetProperty(key);
                return pi.GetValue(item, null).ToString().Contains(txtbFilter.Text, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void radioButtons_CheckedChanged(object sender, RoutedEventArgs e)
        {
            cmbSearchParameter.Items.Clear();
            dgDataTable.Columns.Clear();
            dgDataTable.Items.Clear();

            CheckedRadioButton = ((RadioButton)sender).Content.ToString();
            btnToevoegen.IsEnabled = true;

            switch (CheckedRadioButton)
            {    
                case "Bedrijven":
                    SavedCompanyId = 0;
                    SavedVisitorId = 0;
                    SavedParkingId = 0;
                    try
                    {
                        IReadOnlyList<Company> companies = _cm.GetCompanies();

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in companies[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(CompanyParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = CompanyParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }
                        AddActionButtonsColumn(true, false, false, false, true);

                        foreach (object item in companies)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen bedrijven teruggevonden!"); }
                        break;
                case "Medewerkers":
                    SavedVisitorId = 0;
                    SavedParkingId = 0;
                    List<Employee> employees = new List<Employee>();
                    try
                    {  
                        if (SavedCompanyId > 0) { employees = _cm.GetEmployeesFromCompanyId(SavedCompanyId).ToList(); }
                        else { employees = _cm.GetEmployees().ToList(); }

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in employees[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(EmployeeParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = EmployeeParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }

                        AddActionButtonsColumn(false, false, false, false, true);

                        foreach (object item in employees)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen medewerkers terugevonden!"); }
                    break;
                case "Bezoekers":
                    SavedCompanyId = 0;
                    SavedVisitorId = 0;
                    SavedParkingId = 0;
                    try
                    {
                        IReadOnlyList<Visitor> visitors = _vm.GetVisitors();

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in visitors[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(VisitorParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = VisitorParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }

                        AddActionButtonsColumn(false, true, false, false, true);

                        foreach (object item in visitors)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen bezoekers terugevonden!"); }
                    break;
                case "Bezoeken":
                    SavedCompanyId = 0;
                    SavedParkingId = 0;
                    List<VisitDTO> visits = new List<VisitDTO>();
                    try
                    {
                        if (SavedVisitorId > 0) { visits = _vm.GetVisitsByVisitorId(SavedVisitorId).ToList(); }
                        else { visits = _vm.GetVisits().ToList(); }

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in visits[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(VisitParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = VisitParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }

                        AddActionButtonsColumn(false, false, false, false, false);
                        btnToevoegen.IsEnabled = false;

                        foreach (object item in visits)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen bezoeken teruggevonden!"); }
                    break;
                case "Parking":
                    SavedCompanyId = 0;
                    SavedVisitorId = 0;
                    SavedParkingId = 0;
                    try
                    {
                        IReadOnlyList<ParkingDTO> parkings = _pm.GetParkings();

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in parkings[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(ParkingParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = ParkingParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }
                        AddActionButtonsColumn(false, false, true, true, true);

                        foreach (object item in parkings)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    } catch (Exception ex){ MessageBox.Show("Geen parkings terugevonden!"); }
                    break;
                case "ParkingDetails":
                    SavedCompanyId = 0;
                    SavedVisitorId = 0;
                    IReadOnlyList<ParkingDetailDTO> parkingdetails = new List<ParkingDetailDTO>();
                    try
                    {
                        if (SavedParkingId > 0) { parkingdetails = _pm.GetParkingDetails(SavedParkingId).ToList(); }
                        else { parkingdetails = _pm.GetParkingDetails().ToList(); }

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in parkingdetails[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(ParkingDetailParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = ParkingDetailParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }
                        AddActionButtonsColumn(false, false, false, false, true);

                        foreach (object item in parkingdetails)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen parkingdetails teruggevonden!"); }
                    break;
                case "ParkingContracten":
                    SavedCompanyId = 0;
                    SavedVisitorId = 0;
                    IReadOnlyList<ParkingContractDTO> parkingcontracten = new List<ParkingContractDTO>();
                    try
                    {
                        if (SavedParkingId > 0) { parkingcontracten = _pm.GetParkingContracts(SavedParkingId).ToList(); }
                        else { parkingcontracten = _pm.GetParkingContracts().ToList(); }

                        cmbSearchParameter.Items.Add("All");
                        foreach (string param in parkingcontracten[0].GetType().GetProperties().Select(x => x.Name).ToList())
                        {
                            cmbSearchParameter.Items.Add(ParkingContractParameterDictionary[param]);

                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = ParkingContractParameterDictionary[param];
                            textColumn.Binding = new Binding(param);
                            dgDataTable.Columns.Add(textColumn);
                        }
                        AddActionButtonsColumn(false, false, false, false, true);

                        foreach (object item in parkingcontracten)
                        {
                            dgDataTable.Items.Add(item);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Geen parkingcontracten teruggevonden!"); }
                    break;
                default:
                    break;
            }

            
            cmbSearchParameter.SelectedIndex = 0;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            switch (CheckedRadioButton)
            {
                case "Bedrijven":
                    BedrijfFormWindow bfw = new BedrijfFormWindow(_cm);
                    bfw.ShowDialog();
                    rbBedrijven.IsChecked = false;
                    rbBedrijven.IsChecked = true;
                    break;
                case "Medewerkers":
                    MedewerkerFormWindow mfw = new MedewerkerFormWindow(_cm);
                    mfw.ShowDialog();
                    rbMedewerkers.IsChecked = false;
                    rbMedewerkers.IsChecked = true;
                    break;
                case "Bezoekers":
                    BezoekerFormWindow brfw = new BezoekerFormWindow(_vm);
                    brfw.ShowDialog();
                    rbBezoekers.IsChecked = false;
                    rbBezoekers.IsChecked = true;
                    break;
                case "Bezoeken":
                    BezoekFormWindow bkfw = new BezoekFormWindow(_cm, _vm);
                    bkfw.ShowDialog();
                    rbBezoeken.IsChecked = false;
                    rbBezoeken.IsChecked = true;
                    break;
                case "Parking":
                    ParkingFormWindow pkfw = new ParkingFormWindow(_pm);
                    pkfw.ShowDialog();
                    rbParking.IsChecked = false;
                    rbParking.IsChecked = true;
                    break;
                case "ParkingDetails":
                    ParkingDetailsFormWindow pdfw = new ParkingDetailsFormWindow(_pm);
                    pdfw.ShowDialog();
                    rbParkingDetails.IsChecked = false;
                    rbParkingDetails.IsChecked = true;
                    break;
                case "ParkingContracten":
                    ParkingContractenFormWindow pcfw = new ParkingContractenFormWindow(_pm,_cm);
                    pcfw.ShowDialog();
                    rbParkingContracten.IsChecked = false;
                    rbParkingContracten.IsChecked = true;
                    break;
                default:
                    break;
            }

        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (dgDataTable.SelectedValue.GetType().Name)
            {
                case nameof(Company):
                    BedrijfFormWindow bfw = new BedrijfFormWindow(_cm, (Company)dgDataTable.SelectedValue);
                    bfw.ShowDialog();
                    rbBedrijven.IsChecked = false;
                    rbBedrijven.IsChecked = true;
                    break;
                case nameof(Employee):
                    MedewerkerFormWindow mfw = new MedewerkerFormWindow(_cm, (Employee)dgDataTable.SelectedValue);
                    mfw.ShowDialog();
                    rbMedewerkers.IsChecked = false;
                    rbMedewerkers.IsChecked = true;
                    break;
                case nameof(Visitor):
                    BezoekerFormWindow brfw = new BezoekerFormWindow(_vm, (Visitor)dgDataTable.SelectedValue);
                    brfw.ShowDialog();
                    rbBezoekers.IsChecked = false;
                    rbBezoekers.IsChecked = true;
                    break;
                case nameof(VisitDTO):
                    BezoekFormWindow bkfw = new BezoekFormWindow(_cm, _vm, (VisitDTO)dgDataTable.SelectedValue);
                    bkfw.ShowDialog();
                    rbBezoeken.IsChecked = false;
                    rbBezoeken.IsChecked = true;
                    break;
                case nameof(ParkingDTO):
                    ParkingFormWindow pkfw = new ParkingFormWindow(_pm, (ParkingDTO)dgDataTable.SelectedValue);
                    pkfw.ShowDialog();
                    rbParking.IsChecked = false;
                    rbParking.IsChecked = true;
                    break;
                case nameof(ParkingContractDTO):
                    ParkingContractenFormWindow pkcfw = new ParkingContractenFormWindow(_pm,_cm, (ParkingContractDTO)dgDataTable.SelectedValue);
                    pkcfw.ShowDialog();
                    rbParkingContracten.IsChecked = false;
                    rbParkingContracten.IsChecked = true;
                    break;
                case nameof(ParkingDetailDTO):
                    ParkingDetailsFormWindow pkdfw = new ParkingDetailsFormWindow(_pm, (ParkingDetailDTO)dgDataTable.SelectedValue);
                    pkdfw.ShowDialog();
                    rbParkingDetails.IsChecked = false;
                    rbParkingDetails.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            string typeName = dgDataTable.SelectedValue.GetType().Name;
            MessageBoxResult result = MessageBox.Show("Bent u zeker dat u deze record wilt verwijderen?", "Bevestiging", System.Windows.MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                switch (typeName)
                {
                    case nameof(Company):
                        try
                        {
                            _cm.RemoveCompany((Company)dgDataTable.SelectedValue);
                            rbBedrijven.IsChecked = false;
                            rbBedrijven.IsChecked = true;
                        } catch (Exception ex){ MessageBox.Show("Het bedrijf bevat medewerkers!", "Error"); }
                        break;
                    case nameof(Employee):
                        _cm.RemoveEmployee((Employee)dgDataTable.SelectedValue);
                        rbMedewerkers.IsChecked = false;
                        rbMedewerkers.IsChecked = true;
                        break;
                    case nameof(Visitor):
                        _vm.DeleteVisitor((Visitor)dgDataTable.SelectedValue);
                        rbBezoekers.IsChecked = false;
                        rbBezoekers.IsChecked = true;
                        break;
                    case nameof(VisitDTO):
                        _vm.DeleteVisit((VisitDTO)dgDataTable.SelectedValue);
                        rbBezoeken.IsChecked = false;
                        rbBezoeken.IsChecked = true;
                        break;
                    case nameof(ParkingDTO):
                        ParkingDTO pDTO = ((ParkingDTO)dgDataTable.SelectedValue);
                        _pm.RemoveParking(pDTO.ID);
                        rbParking.IsChecked = false;
                        rbParking.IsChecked = true;
                        break;
                    case nameof(ParkingDetailDTO):
                        ParkingDetailDTO pdDTO = ((ParkingDetailDTO)dgDataTable.SelectedValue);
                        _pm.RemoveParkingDetail(pdDTO.Id);
                        rbParkingDetails.IsChecked = false;
                        rbParkingDetails.IsChecked = true;
                        break;
                    case nameof(ParkingContractDTO):
                        ParkingContractDTO pcDTO = ((ParkingContractDTO)dgDataTable.SelectedValue);
                        _pm.RemoveParkingContract(pcDTO.Id);
                        rbParkingContracten.IsChecked = false;
                        rbParkingContracten.IsChecked = true;
                        break;
                    default:
                        break;
                }
            } 
        }

        private void EmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            SavedCompanyId = ((Company)dgDataTable.SelectedValue).ID;
            ((RadioButton)stpFilterRadioButtons.Children[1]).IsChecked = true;
        }

        private void VisitButton_OnClick(object sender, RoutedEventArgs e)
        {
            SavedVisitorId = ((Visitor)dgDataTable.SelectedValue).Id;
            ((RadioButton)stpFilterRadioButtons.Children[3]).IsChecked = true;
        }

        private void ParkingDetailButton_OnClick(object sender, RoutedEventArgs e)
        {
            SavedParkingId = ((ParkingDTO)dgDataTable.SelectedValue).ID;
            ((RadioButton)stpFilterRadioButtons.Children[6]).IsChecked = true;
        }
        
        private void ParkingContractButton_OnClick(object sender, RoutedEventArgs e)
        {
            SavedParkingId = ((ParkingDTO)dgDataTable.SelectedValue).ID;
            ((RadioButton)stpFilterRadioButtons.Children[5]).IsChecked = true;
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

        private void AddActionButtonsColumn(bool showEmployeeButton, bool showVisitButton, bool showContractButton, bool showDetailButton, bool showEditButton)
        {
            DataGridTemplateColumn col = new DataGridTemplateColumn();
            col.Header = "Acties";
            DataTemplate dt = new DataTemplate();
            var sp = new FrameworkElementFactory(typeof(WrapPanel));
            sp.SetValue(WrapPanel.OrientationProperty, Orientation.Horizontal);
            sp.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);

            if (showEmployeeButton) { sp.AppendChild(AddButton("👤", "EMPLOYEE_ACTION")); }
            if (showVisitButton) { sp.AppendChild(AddButton("📒", "VISIT_ACTION")); }
            if (showContractButton) { sp.AppendChild(AddButton("📜", "CONTRACT_ACTION")); }
            if (showDetailButton) { sp.AppendChild(AddButton("📒", "DETAIL_ACTION")); }
            if (showEditButton) { sp.AppendChild(AddButton("🖉", "EDIT_ACTION")); }
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
                case "VISIT_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightSalmon));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(VisitButton_OnClick));
                    break;
                case "DETAIL_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightBlue));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(ParkingDetailButton_OnClick));
                    break;
                case "CONTRACT_ACTION":
                    btn.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.LightSalmon));
                    btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(ParkingContractButton_OnClick));
                    break;
                default:
                    break;
            }
            return btn;
        }

    }
}
