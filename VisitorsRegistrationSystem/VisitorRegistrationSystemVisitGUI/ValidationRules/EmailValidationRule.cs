using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VisitorsRegistrationSystemBL.Checkers;

namespace VisitorRegistrationSystemVisitGUI.ValidationRules
{
    class EmailValidationRule : ValidationRule {

        /// <summary>
        /// A validation rule that checks whether the email string inside a textbox has the correct format.
        /// </summary>
        /// <param name="value">de email string within the textbox</param>
        /// <returns>The ValidationResult shows whether the email string inside thet textbox has the correct format</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            string valueToValidate = value as string;
            if (EmailChecker.IsValid(valueToValidate)) {
                return ValidationResult.ValidResult;
            } else {
                return new ValidationResult(false, "Ongeldige e-mail");
            }
        }
    }
}
