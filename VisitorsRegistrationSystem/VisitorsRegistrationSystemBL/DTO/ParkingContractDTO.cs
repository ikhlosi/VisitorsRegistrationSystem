using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.DTO
{
    public class ParkingContractDTO
    {
        public ParkingContractDTO(int id, int companyId, int spaces, DateTime startDate, DateTime endDate, int parkingId)
        {
            Id = id;
            CompanyId = companyId;
            Spaces = spaces;
            StartDate = startDate;
            EndDate = endDate;
            ParkingId = parkingId;
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int Spaces { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParkingId { get; set; }

        public override string ToString()
        {
            return $"{Id} {CompanyId} {Spaces} {StartDate} {EndDate} {ParkingId}";
        }
    }
}
