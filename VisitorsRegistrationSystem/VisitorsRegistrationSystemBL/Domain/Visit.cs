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
        public int Id { get; private set; }
        public Visitor Visitor { get; private set; }
        public Company VisitedCompany { get; private set; }
        public Employee VisitedEmployee { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Visit(Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime)
        {
            VisitorSet(visitor);
            VisitCompanySet(visitedCompany);
            VisitEmployeeSet(visitedEmployee);
            StartTimeSet(startTime);
        }
        public Visit(int id,Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime)
        {
            setId(id);
            VisitorSet(visitor);
            VisitCompanySet(visitedCompany);
            VisitEmployeeSet(visitedEmployee);
            StartTimeSet(startTime);
        }

        public void VisitorSet(Visitor visitor)
        {
            Visitor = visitor ?? throw new VisitException("Visit - Visitor is null");
        }
        public void setId(int id)
        {
            if (id < 1) throw new VisitException("Visit - id smaller than 1");
            this.Id = id;
        }

        public void VisitCompanySet(Company visitedCompany)
        {
            VisitedCompany = visitedCompany ?? throw new VisitException("Visit - Visited Company is null");
        }

        public void VisitEmployeeSet(Employee visitedEmployee)
        {
            //if (!VisitedCompany.GetEmployees().Contains(visitedEmployee)) throw new VisitException("Visit - Employee not part of company");
            VisitedEmployee = visitedEmployee ?? throw new VisitException("Visit - Visited Employee is null");
        }

        public void StartTimeSet(DateTime startTime)
        {
            if (startTime < DateTime.Now) throw new VisitException("Visit - Start time is too late");
            StartTime = startTime;
        }

        public void EndSet(DateTime endTime)
        {
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

        public override string ToString()
        {
            return ("VisitId: " + Id + " startTime " + StartTime.ToString() + " endTime " + EndTime.ToString());
        }
    }
}
