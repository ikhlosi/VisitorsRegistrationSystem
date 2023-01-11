using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    /// <summary>
    /// This class represents the details of a parking spot.
    /// </summary>
    public class ParkingDetail
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LicensePlate { get; set; }
        public Company VisitedCompany { get; set; }
        public int ParkingId { get; set; }
        public ParkingDetail(int id, DateTime startTime, DateTime endTime, string licensePlate, Company visitedCompany,int parkingId)
        {
            SetID(id);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetLicensePlate(licensePlate);
            SetVisitedCompany(visitedCompany);
            SetParkingId(parkingId);
        }
        public ParkingDetail(DateTime startTime, DateTime endTime, string licensePlate, Company visitedCompany,int parkingId)
        {
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetLicensePlate(licensePlate);
            SetVisitedCompany(visitedCompany);
            SetParkingId(parkingId);
        }

        public void SetID(int id)
        {
            if (id == 0 || id < 0) throw new ParkingException("ParkingDetail - SetID - ID is null");
            ID = id;
        }
        public void SetStartTime(DateTime startTime)
        {
            StartTime = startTime;
        }
        public void SetEndTime(DateTime endTime)
        {
            if (endTime == null) throw new ParkingException("ParkingDetail - SetEndTime - End time is null");
            if (endTime < StartTime) throw new ParkingException("ParkingDetail - SetEndTime - End time is smaller than start time");
            EndTime = endTime;
        }
        
        public void SetLicensePlate(string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate)) throw new ParkingException("ParkingDetail - SetLicensePlate - License plate is null");
            LicensePlate = licensePlate;
        }
        public void SetVisitedCompany(Company visitedCompany)
        {
            if (visitedCompany == null ) throw new ParkingException("ParkingDetail - SetVisitedCompany - Visited company is null");
            VisitedCompany = visitedCompany;
        }

        public void SetParkingId(int parkingId)
        {
            if (parkingId < 1) throw new ParkingException("ParkingDetail - SetParkingId - Parking ID is smaller than 1");
            ParkingId = parkingId;
        }

        /// <summary>
        /// This method compares 2 parkingdetail objects to indicate equality.
        /// The objects are considered equal if the following properties are equal:
        /// <list type="bullet">
        /// <item>
        /// <description>ID.</description>
        /// </item>
        /// <item>
        /// <description>StartTime.</description>
        /// </item>
        /// <item>
        /// <description>EndTime.</description>
        /// </item>
        /// <item>
        /// <description>LicensePlate.</description>
        /// </item>
        /// <item>
        /// <description>VisitedCompany.</description>
        /// </item>
        /// <item>
        /// <description>ParkingId.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="obj">The parkingdetail object to compare with.</param>
        /// <returns>A bool indicating whether the objects are equal.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ParkingDetail detail &&
                   ID == detail.ID &&
                   StartTime == detail.StartTime &&
                   EndTime == detail.EndTime &&
                   LicensePlate == detail.LicensePlate &&
                   VisitedCompany == detail.VisitedCompany &&
                   ParkingId == detail.ParkingId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, StartTime, EndTime, LicensePlate, VisitedCompany);
        }
    }
}
