using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public class ParkingDetailFactory
    {
        public static ParkingDetail MakeParkingDetail(int? id,DateTime startTime,DateTime endtime ,string licensePlate,int visitedCompanyID,int parkingId)
        {
            try
            {
                ParkingDetail pd = new ParkingDetail(startTime, endtime, licensePlate, visitedCompanyID,parkingId);
                if (id.HasValue) pd.SetID(id.Value);
                return pd;
            }
            catch (Exception e)
            {
                ParkingException ex = new ParkingException("ParkingDetailFactory - MakeParkingDetail", e);
                ex.Data.Add("ParkingDetail ID", id);
                ex.Data.Add("Parking starttime", startTime);
                ex.Data.Add("Parking endtime", endtime);
                ex.Data.Add("Parking licenseplate", licensePlate);
                ex.Data.Add("Parking visitedCompany ID", visitedCompanyID);
                ex.Data.Add("Parking ID", parkingId);
                throw ex;
            }
        }
    }
}
