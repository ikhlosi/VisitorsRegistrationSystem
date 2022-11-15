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
        internal Visit(string visitorName, string visitorEmail, string visitorCompany, Company visitedCompany, Employee visitedEmployee, DateTime startTime) {
            VisitorName = visitorName;
            VisitorEmail = visitorEmail;
            VisitorCompany = visitorCompany;
            VisitedCompany = visitedCompany;
            VisitedEmployee = visitedEmployee;
            StartTime = startTime;
        }

        public int Id { get; private set; }
        public string VisitorName { get; private set; }
        public string VisitorEmail { get; private set; }
        public string VisitorCompany { get; private set; }
        public Company VisitedCompany { get; private set; }
        public Employee VisitedEmployee { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        internal void SetVisitorName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new VisitException($"{this.GetType()}: {System.Reflection.MethodBase.GetCurrentMethod().Name} - Name is null or whitespace");
            this.VisitorName = name;
        }

        internal void SetVisitorEmail (string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitException($"{this.GetType()}: {System.Reflection.MethodBase.GetCurrentMethod().Name} - Email is null or whitespace");
            if(!EmailChecker.IsValid(email)) throw new VisitException($"{this.GetType()}: {System.Reflection.MethodBase.GetCurrentMethod().Name} - Email format invalid");
            this.VisitorEmail = email;
        }

        internal void SetVisitorCompany(string company)
        {
            if (string.IsNullOrWhiteSpace(company)) throw new VisitException($"{this.GetType()}: {System.Reflection.MethodBase.GetCurrentMethod().Name} - VisitorCompany is null or whitespace");
            this.VisitorCompany = company;
        }

        internal void SetId(int id)
        {
            if (id < 1) throw new VisitException("Visit - id smaller than 1");
            this.Id = id;
        }

        internal void SetVisitedCompany(Company visitedCompany)
        {
            VisitedCompany = visitedCompany ?? throw new VisitException("Visit - Visited Company is null");
        }

        internal void SetVisitedEmployee(Employee visitedEmployee)
        {
            //if (!VisitedCompany.GetEmployees().Contains(visitedEmployee)) throw new VisitException("Visit - Employee not part of company");
            VisitedEmployee = visitedEmployee ?? throw new VisitException("Visit - Visited Employee is null");
        }

        internal void SetStartTime(DateTime startTime)
        {
            if (startTime < DateTime.Now) throw new VisitException("Visit - Start time is too late");
            StartTime = startTime;
        }

        public void SetEndTime(DateTime endTime)
        {
            // todo: checks
            EndTime = endTime;
        }

        public bool IsSame(Visit otherVisit) {
            return (this.Id == otherVisit.Id) &&
                (this.VisitorName == otherVisit.VisitorName) &&
                (this.VisitorEmail == otherVisit.VisitorEmail) &&
                (this.VisitorCompany == otherVisit.VisitorCompany) &&
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
            return $"VisitId: {this.Id} - StartTime: {this.StartTime.ToString()} - Visitor: {this.VisitorName}";
        }
    }
}
