using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Visit
    {
        public Visitor Visitor { get; private set; }
        public Company VisitedCompany { get; private set; }
        public Employee VisitedEmployee { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Visit(Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime, DateTime endTime)
        {
            VisitorSet(visitor);
            VisitCompanySet(visitedCompany);
            VisitEmployeeSet(visitedEmployee);
            TimeSet(startTime,endTime);
        }

        private void VisitorSet(Visitor visitor)
        {
            Visitor = visitor ?? throw new VisitException("Visit - Visitor is null");
        }

        private void VisitCompanySet(Company visitedCompany)
        {
            VisitedCompany = visitedCompany ?? throw new VisitException("Visit - Visited Company is null");
        }

        private void VisitEmployeeSet(Employee visitedEmployee)
        {
            VisitedEmployee = visitedEmployee ?? throw new VisitException("Visit - Visited Employee is null");
        }

        private void TimeSet(DateTime startTime, DateTime endTime)
        {
            if (startTime > DateTime.Now) throw new VisitException("Visit - Start time is too late");
            if (endTime < startTime) throw new VisitException("Visit - End time earlier than Start time");
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            return obj is Visit visit &&
                   EqualityComparer<Visitor>.Default.Equals(Visitor, visit.Visitor) &&
                   StartTime == visit.StartTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Visitor, StartTime);
        }
    }
}
