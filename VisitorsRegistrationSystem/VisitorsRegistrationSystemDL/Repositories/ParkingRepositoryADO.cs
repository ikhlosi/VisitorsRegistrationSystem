using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class ParkingRepositoryADO : IParkingRepository
    {
        public Parking GetParkingById(int iD)
        {
            throw new NotImplementedException();
        }

        public List<Parking> GetParkings()
        {
            throw new NotImplementedException();
        }

        public bool ParkingContractExistsInDB(ParkingContract parkingContract)
        {
            throw new NotImplementedException();
        }

        public bool ParkingContractExistsInDB(int id)
        {
            throw new NotImplementedException();
        }

        public bool ParkingExistsInDB(Parking parking)
        {
            throw new NotImplementedException();
        }

        public bool ParkingExistsInDB(int parkingID)
        {
            throw new NotImplementedException();
        }

        public void RemoveParkingContractFromDB(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveParkingFromDB(int iD)
        {
            throw new NotImplementedException();
        }

        public void UpdateParking(Parking parking)
        {
            throw new NotImplementedException();
        }

        public ParkingContract WriteParkingContractInDB(ParkingContract parkingContract)
        {
            throw new NotImplementedException();
        }

        public Parking WriteParkingInDB(Parking parking)
        {
            throw new NotImplementedException();
        }
    }
}
