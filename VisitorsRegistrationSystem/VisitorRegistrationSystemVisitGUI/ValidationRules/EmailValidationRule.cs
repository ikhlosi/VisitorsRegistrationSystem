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
