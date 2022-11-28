using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorRegistrationSystemVisitGUI.Model {
    internal class VisitorData {
		private string _firstName;
		private string _lastName;
		private string _email;
		private string _visitorCompany;

		public string FirstName {
			get { return _firstName; }
			set {
				//if (string.IsNullOrWhiteSpace(value) || value.Length < 6) throw new ArgumentException("invalid name");
				_firstName = value;
				// OnPropertyChanged();
			}
		}
		public string LastName {
			get { return _lastName; }
			set { 
				_lastName = value;
				// OnPropertyChanged();
			}
		}
		public string Email {
			get { return _email; }
			set { _email = value; }
		}
		public string VisitorCompany {
			get { return _visitorCompany; }
			set { _visitorCompany = value; }
		}

	}
}
