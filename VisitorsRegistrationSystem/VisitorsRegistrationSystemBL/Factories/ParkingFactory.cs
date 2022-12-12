using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public class ParkingFactory
    {
        public static Parking MakeParking(int? id,int occupiedSpaces, bool full, List<ParkingContract>? parkingContracts, List<ParkingDetail>? parkingDetails, int totalSpaces)
        {
            try
            {
                Parking p = new Parking(occupiedSpaces, false, totalSpaces);
                if (parkingContracts != null) p.SetParkingContracts(parkingContracts);
                if (parkingDetails != null) p.SetParkingDetails(parkingDetails);
                if (id.HasValue) p.SetID(id.Value);
                return p;
            }
            catch (Exception e)
            {
                ParkingException ex = new ParkingException("ParkingFactory - MakeParking", e);
                ex.Data.Add("Parking ID", id);
                ex.Data.Add("Parking occupiedSpaces", occupiedSpaces);
                ex.Data.Add("Parking parkingContracts", parkingContracts);
                ex.Data.Add("Parking parkingDetails", parkingDetails);
                ex.Data.Add("Parking totalSpaces", totalSpaces);

                throw ex;
            }
        }   
    }
}