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
    /// <summary>
    /// This class represents a visitor.
    /// </summary>
    public class Visitor
    {
        public Visitor(string name, string email, string company)
        {
            SetName(name);
            SetEmail(email);
            SetVisitorCompany(company);
        }
        public Visitor()
        {
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string VisitorCompany { get; private set; }


        internal void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VisitorException("Visitor - SetName - Name is null or whitespace");
            this.Name = name;
                
        }

        internal void SetEmail (string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitorException("Vistor - SetEmail - Email is null or whitespace");
            if(!EmailChecker.IsValid(email)) throw new VisitorException("Visitor - SetEmail - Email format invalid");
            this.Email = email;
        }

        internal void SetVisitorCompany(string company)
        {
            if (string.IsNullOrWhiteSpace(company)) throw new VisitorException("Visitor -SetVisitorCompany - Company is null or whitespace");
            this.VisitorCompany = company;
        } 

        public void SetId(int id)
        {
            if (id <= 0) throw new VisitorException("Visitor - SetId - invalid Id");
            this.Id = id;
        }
         
        /// <summary>
        /// This method compares 2 visitor objects to indicate equality.
        /// The objects are considered equal if the following properties are equal:
        /// <list type="bullet">
        /// <item>
        /// <description>Id.</description>
        /// </item>
        /// <item>
        /// <description>Name.</description>
        /// </item>
        /// <item>
        /// <description>Email.</description>
        /// </item>
        /// <item>
        /// <description>VisitorCompany.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="obj">The visitor object to compare with.</param>
        /// <returns>A bool indicating whether the objects are equal.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Visitor visitor &&
                   Id == visitor.Id &&
                   Name == visitor.Name &&
                   Email == visitor.Email &&
                   VisitorCompany == visitor.VisitorCompany;
        }

        /// <summary>
        /// This method checks whether the properties of this visitor object
        /// are the same as the properties of another visitor object.
        /// </summary>
        /// <param name="otherVisitor">The other visitor to compare with.</param>
        /// <returns>A bool indicating whether the properties of both objects are equal.</returns>
        /// <exception cref="VisitorException">
        /// Thrown when the argument is null.
        /// </exception>
        public bool IsSame(Visitor otherVisitor)
        {
            if (otherVisitor == null) throw new VisitorException("Visitor - IsSame - argument is null");
            return (this.Id == otherVisitor.Id) && (this.Name == otherVisitor.Name) && (this.Email == otherVisitor.Email) && (this.VisitorCompany == otherVisitor.VisitorCompany);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, VisitorCompany);
        }

        /// <summary>
        /// This method gives the string representation of a visitor object.
        /// </summary>
        /// <returns>A string containing the Id, name and e-mail of the visitor.</returns>
        public override string? ToString()
        {
            return $"[{Id}] {Name}: {Email}";
        }
    }
}
