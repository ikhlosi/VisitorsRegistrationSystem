using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public static class ParkingContractFactory
    {
        public static ParkingContract MakeParkingContract(int? id,Company visitedCompany, DateTime startTime, DateTime endTime, int reservedSpace)
        {
            try
            {
                ParkingContract pc = new ParkingContract(visitedCompany, startTime, endTime, reservedSpace) ;
                if (id.HasValue) pc.SetID(id.Value);
                return pc;
            }
            catch (Exception e)
            {
                ParkingException ex = new ParkingException("ParkingContractFactory - MakeParkingContract", e);
                ex.Data.Add("ParkingContract ID", id);
                ex.Data.Add("VisitedCompany", visitedCompany);
                ex.Data.Add("StartTime", startTime);
                ex.Data.Add("EndTime", endTime);
                ex.Data.Add("Parking", reservedSpace);
                throw ex;
            }
        }
    }
}
