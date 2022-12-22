using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;

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
        IReadOnlyList<ParkingDTO> GetParkings();
        #endregion
        
        #region ParkingContract
        ParkingContract WriteParkingContractInDB(ParkingContract parkingContract);
        ParkingContract GetParkingContractById(int iD);
        IReadOnlyList<ParkingContractDTO> GetParkingContracts();
        bool ParkingContractExistsInDB(int id);
        void RemoveParkingContractFromDB(int id);
        void UpdateParkingContract(ParkingContract parkingContract);
        IReadOnlyList<ParkingContractDTO> GetParkingContracts(int parkingId);

        #endregion

        #region ParkingDetail
        bool ParkingDetailExistsInDB(int id);
        void RemoveParkingDetailFromDB(int id);
        void UpdateParkingDetail(ParkingDetail parkingDetail);
        ParkingDetail WriteParkingDetailInDB(ParkingDetail parkingDetail);
        ParkingDetail GetParkingDetailById(int iD);
        IReadOnlyList<ParkingDetailDTO> GetParkingDetails();
        IReadOnlyList<ParkingDetailDTO> GetParkingDetails(int parkingId);
        #endregion
    }
}
