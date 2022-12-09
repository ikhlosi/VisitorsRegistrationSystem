using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class ParkingContract
    {
        public ParkingContract(Company company, DateTime startDate, DateTime endDate, int reservedSpace)
        {
            SetCompany(company);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetReservedSpace(reservedSpace);
        }

        public ParkingContract(int iD, Company company, DateTime startDate, DateTime endDate, int reservedSpace)
        {
            SetID(iD);
            SetCompany(company);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetReservedSpace(reservedSpace);
        }

        public int ID { get; private set; }
        public Company Company { get; set; }
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public int ReservedSpace { get; set; }
        
        public void SetID(int id)
        {
            if (id == 0) throw new ParkingException("Parking - SetId - ID is null");
            this.ID = id;
        }
        public void SetCompany(Company company)
        {
            Company = company ?? throw new VisitException("Parking - SetCompany -  Company is null");
        }
        internal void SetStartDate(DateTime startDate)
        {
            if (startDate < DateTime.Now) throw new ParkingException("Parking - SetStartDate - Start date is too early");
            StartDate = startDate;
        }
        internal void SetEndDate(DateTime endDate)
        {
            if (endDate < DateTime.Now) throw new ParkingException("Parking - SetEndDate - End date is too early");
            EndDate = endDate;
        }
        
        internal void SetReservedSpace(int reservedSpace)
        {
            if (reservedSpace < 1) throw new ParkingException("Parking - SetReservedSpace - Reserved space is too small");
            ReservedSpace = reservedSpace;
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
