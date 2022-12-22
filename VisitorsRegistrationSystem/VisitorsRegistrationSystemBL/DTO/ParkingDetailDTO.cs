using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.DTO
{
    public class ParkingDetailDTO
    {
        public ParkingDetailDTO(int id, DateTime startTime, DateTime endTime, string licensePlate, int visitedCompanyId, int parkingId)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            LicensePlate = licensePlate;
            VisitedCompanyId = visitedCompanyId;
            ParkingId = parkingId;
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LicensePlate { get; set; }
        public int VisitedCompanyId { get; set; }
        public int ParkingId { get; set; }

        public override string ToString()
        {
            return $"{Id} {StartTime} {EndTime} {LicensePlate} {VisitedCompanyId} {ParkingId}";
        }
    }
}
