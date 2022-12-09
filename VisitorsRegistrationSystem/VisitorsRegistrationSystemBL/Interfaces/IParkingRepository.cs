using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces
{
    public interface IParkingRepository
    {
        Parking GetParkingById(int iD);
        ParkingContract GetParkingContractById(int iD);
        List<ParkingContract> GetParkingContracts();
        ParkingDetail GetParkingDetailById(int iD);
        List<ParkingDetail> GetParkingDetails();
        List<Parking> GetParkings();
        bool ParkingContractExistsInDB(ParkingContract parkingContract);
        bool ParkingContractExistsInDB(int id);
        bool ParkingDetailExistsInDB(ParkingDetail parkingDetail);
        bool ParkingDetailExistsInDB(int id);
        bool ParkingExistsInDB(Parking parking);
        bool ParkingExistsInDB(int parkingID);
        void RemoveParkingContractFromDB(int id);
        void RemoveParkingDetailFromDB(int id);
        void RemoveParkingFromDB(int iD);
        void UpdateParking(Parking parking);
        void UpdateParkingContract(ParkingContract parkingContract);
        void UpdateParkingDetail(ParkingDetail parkingDetail);
        ParkingContract WriteParkingContractInDB(ParkingContract parkingContract);
        ParkingDetail WriteParkingDetailInDB(ParkingDetail parkingDetail);
        Parking WriteParkingInDB(Parking parking);
    }
}
