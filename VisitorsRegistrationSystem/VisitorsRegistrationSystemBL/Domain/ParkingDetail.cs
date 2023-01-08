﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
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
            if (startTime == null) throw new ParkingException("ParkingDetail - SetStartTime - Start time is null");
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
            // todo: check if license plate format is good?
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
