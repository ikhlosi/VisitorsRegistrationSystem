using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Visitor
    {
        public Visitor(string name, string email)
        {
            setName(name);
            setEmail(email);
        }
        public Visitor(string name, string email, Company visitorCompany) : this(name,email)
        {
            setVisitorCompany(visitorCompany);
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public Company VisitorCompany { get; private set; }

        internal void setName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VisitorException("Visitor - Name is null or whitespace");
            this.Name = name;
        }

        internal void setEmail (string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|be|nl)$";
            if (string.IsNullOrWhiteSpace(email)) throw new VisitorException("Visitor - Email is null or whitespace");
            // TODO check wether this formatchecker actually works
            if(!Regex.IsMatch(email, regex, RegexOptions.IgnoreCase)) throw new VisitorException("Visitor - Email format invalid");
            this.Email = email;
        }

        internal void setVisitorCompany(Company company)
        {
            if (company == null) throw new VisitorException("Visitor - Visitorcompany is null");
            this.VisitorCompany = company;
        }

        public override bool Equals(object? obj)
        {
            return obj is Visitor visitor &&
                   Email == visitor.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email);
        }
    }
}
