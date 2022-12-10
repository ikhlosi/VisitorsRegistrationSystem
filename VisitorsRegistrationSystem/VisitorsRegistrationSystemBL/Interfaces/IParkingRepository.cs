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
        #region Parking
        Parking GetParkingById(int iD);
        Parking WriteParkingInDB(Parking parking);
        bool ParkingExistsInDB(int parkingID);
        void RemoveParkingFromDB(int iD);
        void UpdateParking(Parking parking);
        List<Parking> GetParkings();
        #endregion
        
        #region ParkingContract
        ParkingContract WriteParkingContractInDB(ParkingContract parkingContract);
        ParkingContract GetParkingContractById(int iD);
        List<ParkingContract> GetParkingContracts();
        bool ParkingContractExistsInDB(ParkingContract parkingContract);
        bool ParkingContractExistsInDB(int id);
        void RemoveParkingContractFromDB(int id);
        void UpdateParkingContract(ParkingContract parkingContract);
        #endregion

        #region ParkingDetail
        bool ParkingDetailExistsInDB(ParkingDetail parkingDetail);
        bool ParkingDetailExistsInDB(int id);
        void RemoveParkingDetailFromDB(int id);
        void UpdateParkingDetail(ParkingDetail parkingDetail);
        ParkingDetail WriteParkingDetailInDB(ParkingDetail parkingDetail);
        ParkingDetail GetParkingDetailById(int iD);
        List<ParkingDetail> GetParkingDetails();
        #endregion
    }
}
