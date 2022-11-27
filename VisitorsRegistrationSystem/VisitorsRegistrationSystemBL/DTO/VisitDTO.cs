using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.DTO
{
    public class VisitDTO
    {
        public VisitDTO(int visitId, int visitorId, DateTime starTime, DateTime endTime, int companyId, int employeeId)
        {
            this.visitId = visitId;
            this.visitorId = visitorId;
            this.startTime = starTime;
            this.endTime = endTime;
            this.companyId = companyId;
            this.employeeId = employeeId;
        }

        public int visitId { get; set; }
        public int visitorId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int companyId { get; set; }
        public int employeeId { get; set; }

        public override string ToString()
        {
            return $"{visitId} {visitorId} {startTime} {endTime} {companyId} {employeeId}";
        }
    }
}
