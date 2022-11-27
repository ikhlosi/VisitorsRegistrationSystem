using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Checkers;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Visit
    {
        public Visit(Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime) {
            SetVisitor(visitor);
            SetVisitedCompany(visitedCompany);
            SetVisitedEmployee(visitedEmployee);
            SetStartTime(startTime);
        }

        public int Id { get; private set; }
        public Visitor Visitor { get; private set; }
        public Company VisitedCompany { get; private set; }
        public Employee VisitedEmployee { get; private set; }
        public DateTime StartTime { get; private set; }
       
        internal void SetVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitException("Visit - SetVisitor - Visitor is null");
            this.Visitor = visitor;
        }

        internal void SetId(int id)
        {
            if (id < 1) throw new VisitException("Visit - SetId - id smaller than 1");
            this.Id = id;
        }

        internal void SetVisitedCompany(Company visitedCompany)
        {
            VisitedCompany = visitedCompany ?? throw new VisitException("Visit - SetVisitedCompany - Visited Company is null");
        }

        internal void SetVisitedEmployee(Employee visitedEmployee)
        {
            //if (!VisitedCompany.GetEmployees().Contains(visitedEmployee)) throw new VisitException("Visit - Employee not part of company");
            VisitedEmployee = visitedEmployee ?? throw new VisitException("Visit - SetVisitedEmployee - Visited Employee is null");
        }

        internal void SetStartTime(DateTime startTime)
        {
            if (startTime < DateTime.Now) throw new VisitException("Visit - SetStartTime - Start time is too early");
            StartTime = startTime;
        }

      

        public bool IsSame(Visit otherVisit) {
            return (this.Id == otherVisit.Id) &&
                (this.Visitor.Equals(otherVisit.Visitor)) &&
                (this.VisitedCompany == otherVisit.VisitedCompany) &&
                (this.VisitedEmployee == otherVisit.VisitedEmployee) &&
                (this.StartTime == otherVisit.StartTime);
        }

        public override bool Equals(object? obj) {
            return obj is Visit visit &&
                   Id == visit.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"VisitId: {this.Id} - StartTime: {this.StartTime.ToString()} - Visitor: {this.Visitor.Name}";
        }
    }
}
