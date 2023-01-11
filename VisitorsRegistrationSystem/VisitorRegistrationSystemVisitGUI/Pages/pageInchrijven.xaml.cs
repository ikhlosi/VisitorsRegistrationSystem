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
using VisitorRegistrationSystemVisitGUI.ValidationRules;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Managers;

namespace VisitorRegistrationSystemVisitGUI.Pages
{
    /// <summary>
    /// Interaction logic for pageInchrijven.xaml
    /// This class implements a page that is shown when the user wants to start their visit.
    /// </summary>
    public partial class pageInchrijven : Page
    {
        private readonly CompanyManager _cm;
        private VisitManager _vm;

        /// <summary>
        /// This is the constructor: used to initialize the components, data and assign the managers.
        /// </summary>
        /// <param name="cm">the CompanyManager that handles the business logic regarding Companies and Employees</param>
        /// <param name="vm">the VisitManager that handles the business logic regarding Visits and Visitors</param>
        public pageInchrijven(CompanyManager cm, VisitManager vm)
        {
            _cm = cm;
            _vm = vm;
            InitializeComponent();
            InitializeData();
        }

        /// <summary>
        /// this methode initializes the data by populates the cbBedrijven combobox with companies found in our database.
        /// </summary>
        private void InitializeData()
        {
            //cbBedrijfAfspraak.ItemsSource = _cm.GetCompanies();
            //cbBedrijfAfspraak.Items.Insert(0, "Kies een bedrijf");
            //cbBedrijfAfspraak.SelectedIndex = 0;

            cbBedrijfAfspraak.Items.Insert(0, "Kies een bedrijf");
            IReadOnlyList<Company> companies = _cm.GetCompanies();
            foreach (Company c in companies)
            {
                cbBedrijfAfspraak.Items.Add(c);
            }
            cbBedrijfAfspraak.SelectedIndex = 0;
        }

        /// <summary>
        /// The eventHandler is triggered when the btnInschrijven button is clicked so that a new visit can be registered into the system.
        /// </summary>
        /// <param name="sender">the btnInschrijven button when it is clicked</param>
        private void btnInschrijven_Click(object sender, RoutedEventArgs e)
        {
            string visitorName = $"{txtbVoornaam.Text} {txtbAchternaam.Text}";
            string visitorEmail = txtbEmail.Text;
            string visitorCompany = txtbBedrijfBezoeker.Text;
            try {
                Visitor visitor = VisitorFactory.MakeVisitor(null, visitorName, visitorEmail, visitorCompany);
                Company visitedCompany = (Company)cbBedrijfAfspraak.SelectedItem;
                Employee visitedEmployee = (Employee)cbAfspraakMet.SelectedItem;
                Visit visit = VisitFactory.MakeVisit(null, visitor, visitedCompany, visitedEmployee);
                _vm.AddVisit(visit);
                Application.Current.MainWindow.Content = new PageInschrijvenSucces(_cm, _vm);
            } catch (Exception ex) {
                var cmb = new CustomMessageBox("Error Bericht",ex.Message);
                cmb.ShowDialog();
                //MessageBox.Show(ex.Message);
            }

            // todo: btnInschrijven enkel klikbaar wanneer alles (correct) ingevuld
        }

        /// <summary>
        /// the eventHandler is triggered when the back button Button is clicked to go back to the pageMain page.
        /// </summary>
        /// <param name="sender">the Button button when it is clicked</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new pageMain(_cm, _vm);
        }

        private void cbBedrijfAfspraak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cbAfspraakMet.Items.Add(_cm.GetEmployeesFromCompanyId(((Company)cbBedrijfAfspraak.SelectedValue).ID));
            //cbAfspraakMet.Items.Insert(0, "Kies een medewerker");
            //cbAfspraakMet.SelectedIndex = 0;
            cbAfspraakMet.Items.Clear();
            cbAfspraakMet.Items.Insert(0, "Kies een medewerker");
            if (cbBedrijfAfspraak.SelectedIndex != 0)
            {
                IReadOnlyList<Employee> employees = _cm.GetEmployeesFromCompanyId(((Company)cbBedrijfAfspraak.SelectedValue).ID);
                foreach (Employee em in employees)
                {
                    cbAfspraakMet.Items.Add(em);
                }
            } 
            cbAfspraakMet.SelectedIndex = 0;
            EnableButton();
        }

        private void TextBoxes_TextChanged(object sender, EventArgs e) {
            EnableButton();
        }

        /// <summary>
        /// This methode runs all the validation rules over each textbox before it enables the btnInschrijven button to be able to submit a visit.
        /// </summary>
        private void EnableButton() {
            NameValidationRule rule = new NameValidationRule();
            ValidationResult resFirstName = rule.Validate(txtbVoornaam.Text, System.Globalization.CultureInfo.InvariantCulture);
            ValidationResult resLastName = rule.Validate(txtbAchternaam.Text, System.Globalization.CultureInfo.InvariantCulture);
            ValidationResult resCompanyVisitor = rule.Validate(txtbBedrijfBezoeker.Text, System.Globalization.CultureInfo.InvariantCulture);

            EmailValidationRule emailRule = new EmailValidationRule();
            ValidationResult resEmail = emailRule.Validate(txtbEmail.Text, System.Globalization.CultureInfo.InvariantCulture);

            if (resFirstName.IsValid && resLastName.IsValid && resCompanyVisitor.IsValid && resEmail.IsValid && (cbBedrijfAfspraak.SelectedIndex != 0) && (cbAfspraakMet.SelectedIndex != 0)) {
                btnInschrijven.IsEnabled = true;
            } else {
                btnInschrijven.IsEnabled = false;
            }
            //if (!string.IsNullOrWhiteSpace(txtbVoornaam.Text) && (!string.IsNullOrWhiteSpace)) {
            //    btnInschrijven.IsEnabled = true;
            //}
        }

        /// <summary>
        /// The eventHandler is triggered when one of the textboxes are changed, then it will trigger the EnableButton methode.
        /// </summary>
        /// <param name="sender">A textbox where the text has been changed</param>
        private void TextBoxes_TextChanged(object sender, TextChangedEventArgs e) {
            EnableButton();
        }

        /// <summary>
        /// The eventHandler is triggered when the selection of cbAfraak met is changed, then it will trigger the EnableButton methode.
        /// </summary>
        /// <param name="sender">the cbAfraakMet combobox when the selection is changed</param>
        private void cbAfspraakMet_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            EnableButton();
        }
    }
}
