using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public Visitor(string name, string email, Company visitorCompany, int id) : this(name, email)
        {
            setVisitorCompany(visitorCompany);
            setId(id);
        }

        public int Id { get; private set; }
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
            if (string.IsNullOrWhiteSpace(email)) throw new VisitorException("Visitor - Email is null or whitespace");
            if(!EmailChecker.IsValid(email)) throw new VisitorException("Visitor - Email format invalid");
            this.Email = email;
        }

        internal void setVisitorCompany(Company company)
        {
            if (company == null) throw new VisitorException("Visitor - Visitorcompany is null");
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
                   EqualityComparer<Company>.Default.Equals(VisitorCompany, visitor.VisitorCompany);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, VisitorCompany);
        }
    }
}
