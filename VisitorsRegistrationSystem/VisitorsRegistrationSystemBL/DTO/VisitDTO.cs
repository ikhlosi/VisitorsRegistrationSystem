using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.DTO
{
    public class VisitDTO
    {
        public VisitDTO(int visitId, Visitor visitor, DateTime starTime, DateTime? endTime, string company, string employee)
        {
            this.visitId = visitId;
            this.visitor = visitor;
            this.startTime = starTime;
            this.endTime = endTime;
            this.company = company;
            this.employee = employee;
        }

        public int visitId { get; set; }
        public Visitor visitor { get; set; }
        public DateTime startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string company { get; set; }
        public string employee { get; set; }

        public override string ToString()
        {
            return $"{visitId} {visitor} {startTime} {endTime} {company} {employee}";
        }
    }
}
