﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class ParkingContract
    {
        public ParkingContract(Company company, DateTime startDate, DateTime endDate, int reservedSpace,int parkingId)
        {
            SetCompany(company);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetReservedSpace(reservedSpace);
            SetParkingId(parkingId);
        }

        public ParkingContract(int iD, Company company, DateTime startDate, DateTime endDate, int reservedSpace, int parkingId)
        {
            SetID(iD);
            SetCompany(company);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetReservedSpace(reservedSpace);
            SetParkingId(parkingId);
        }

        public int ID { get; private set; }
        public Company Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReservedSpace { get; set; }
        public int parkingId { get; set; }
        
        public void SetID(int id)
        {
            if (id == 0) throw new ParkingException("Parking - SetId - ID is null");
            this.ID = id;
        }
        public void SetCompany(Company company)
        {
            Company = company ?? throw new VisitException("Parking - SetCompany -  Company is null");
        }
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void SetReservedSpace(int reservedSpace)
        {
            if (reservedSpace < 1) throw new ParkingException("Parking - SetReservedSpace - Reserved space is too small");
            ReservedSpace = reservedSpace;
        }

        public void SetParkingId(int parkingId)
        {
            if (parkingId < 1) throw new ParkingException("Parking - SetParkingId - Parking ID is too small");
            this.parkingId = parkingId;
        }
        
        public override bool Equals(object? obj)
        {
            return obj is ParkingContract contract &&
                   ID == contract.ID &&
                   EqualityComparer<Company>.Default.Equals(Company, contract.Company) &&
                   StartDate == contract.StartDate &&
                   EndDate == contract.EndDate &&
                   ReservedSpace == contract.ReservedSpace;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Company, StartDate, EndDate, ReservedSpace);
        }
    }
}
