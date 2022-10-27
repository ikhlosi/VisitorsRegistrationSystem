using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Checkers {
    public static class EmailChecker {
        public static bool IsValid(string email) {
            // string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|be|nl)$";
            string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            // TODO check wether this formatchecker actually works
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}
