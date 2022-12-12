using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Parking
    {
        public Parking(int occupiedSpaces, bool full, int totalSpaces)
        {
            SetOccupiedSpaces(occupiedSpaces);
            SetFull(full);
            SetTotalSpaces(totalSpaces);
        }

        public int ID { get; set; }
        public int TotalSpaces { get; set; }
        public int OccupiedSpaces { get; set; }
        public bool Full { get; set; }
        public List<ParkingContract> ParkingContracts { get; set; }
        public List<ParkingDetail> ParkingDetails { get; set; }

        public void SetID(int id)
        {
            if (id == 0) throw new ParkingException("Parking - SetId - ID is null");
            this.ID = id;
        }
        public void SetTotalSpaces(int totalSpaces)
        {
            if (totalSpaces < 0) throw new ParkingException("Parking - SetTotalSpaces - Total spaces is too small");
            this.TotalSpaces = totalSpaces;
        }
        public void SetOccupiedSpaces(int occupiedSpaces)
        {
            if (occupiedSpaces < 0) throw new ParkingException("Parking - SetOccupiedSpaces - Occupied spaces is too small");
            OccupiedSpaces = occupiedSpaces;
        }
        public void SetFull(bool full)
        {
            Full = full;
        }
        public void SetParkingContracts(List<ParkingContract> parkingContracts)
        {
            ParkingContracts = parkingContracts;
        }
        public void SetParkingDetails(List<ParkingDetail> parkingDetails)
        {
            ParkingDetails = parkingDetails;
        }

        public override bool Equals(object? obj)
        {
            return obj is Parking parking &&
                   ID == parking.ID &&
                   OccupiedSpaces == parking.OccupiedSpaces &&
                   Full == parking.Full &&
                   EqualityComparer<List<ParkingContract>>.Default.Equals(ParkingContracts, parking.ParkingContracts) &&
                   EqualityComparer<List<ParkingDetail>>.Default.Equals(ParkingDetails, parking.ParkingDetails);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, OccupiedSpaces, Full, ParkingContracts, ParkingDetails);
        }
    }
}
