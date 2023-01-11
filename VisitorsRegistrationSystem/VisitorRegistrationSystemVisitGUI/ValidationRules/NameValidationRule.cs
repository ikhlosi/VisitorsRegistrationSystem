using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VisitorRegistrationSystemVisitGUI.ValidationRules {
    internal class NameValidationRule : ValidationRule {

        /// <summary>
        /// A validation rule that checks whether the inputted string inside a textbox is empty or not.
        /// </summary>
        /// <param name="value">de inputed string within the textbox</param>
        /// <returns>The ValidationResult shows whether the textbox is empty or not</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            string valueToValidate = value as string;
            if (string.IsNullOrWhiteSpace(valueToValidate) ) {
                return new ValidationResult(false, "Mag niet leeg zijn");
            }
            return ValidationResult.ValidResult;
        }
    }
}
