using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Checkers;
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
        // needed for moq (visitormanagertest with MOQ)
        public Visitor()
        {
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string VisitorCompany { get; private set; }

        internal void setName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VisitorException("Visitor - Name is null or whitespace");
            this.Name = name;
        }

        internal void setEmail (string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitorException("Visitor - Email is null or whitespace");
            if(!EmailChecker.IsValid(email)) throw new VisitorException("Visitor - Email format invalid");
            this.Email = email;
        }

        internal void setVisitorCompany(string company)
        {
            this.VisitorCompany = company;
        }

        internal void setId(int id)
        {
            if (id <= 0) throw new VisitorException("Visitor - invalid Id");
            this.Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Visitor visitor &&
                   Id == visitor.Id &&
                   Name == visitor.Name &&
                   Email == visitor.Email &&
                   VisitorCompany == visitor.VisitorCompany;
        }
        public bool IsSame(Visitor otherVisitor)
        {
            if (otherVisitor == null) throw new VisitorException("Visitor - IsSame - argument is null");
            return (this.Id == otherVisitor.Id) && (this.Name == otherVisitor.Name) && (this.Email == otherVisitor.Email) && (this.VisitorCompany == otherVisitor.VisitorCompany);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, VisitorCompany);
        }

        public override string? ToString()
        {
            return $"[{Id}] {Name}: {Email}";
        }
    }
}
