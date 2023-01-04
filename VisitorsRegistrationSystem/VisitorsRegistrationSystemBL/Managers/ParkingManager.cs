using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class ParkingManager
    {
        public ParkingManager(IParkingRepository repo)
        {
            _repo = repo;
        }
        private IParkingRepository _repo;
        #region Parking
        public Parking AddParking(Parking parking)
        {
            if (parking == null)
            {
                throw new ParkingManagerException("ParkingManager - AddParking - parking is null.");
            }
            try
            {
                // we're not checking if the parking already exists because:
                // there's no way to identify if a parking is new or not when it doesn't have an id yet
                return _repo.WriteParkingInDB(parking);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - AddParking", ex);
            }
        }
        public void RemoveParking(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - RemoveParking - id is not valid.");
            try
            {
                if (!_repo.ParkingExistsInDB(id)) throw new ParkingManagerException("ParkingManager - RemoveParking - parking does not exist in DB.");
                _repo.RemoveParkingFromDB(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - RemoveParking", ex);
            }
        }
        public void UpdateParking(Parking parking)
        {
            if (parking == null) throw new ParkingManagerException("ParkingManager - UpdateParking - parking is null.");
            try
            {
                if (!_repo.ParkingExistsInDB(parking.ID)) throw new ParkingManagerException("ParkingManager - UpdateParking - parking does not exist in DB.");
                if (_repo.GetParkingById(parking.ID).Equals(parking)) throw new ParkingManagerException("ParkingManager - UpdateParking - parking is not different");
                _repo.UpdateParking(parking);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - UpdateParking", ex);
            }
        }

        public Parking GetParkingById(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - GetParkingById - id is not valid");
            try
            {
                if (!_repo.ParkingExistsInDB(id)) throw new ParkingManagerException("ParkingManager - GetParkingByID - parking does not exist in DB.");
                return _repo.GetParkingById(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingById", ex);
            }
        }
        
        public IReadOnlyList<ParkingDTO> GetParkings()
        {
            try
            {
                return _repo.GetParkings();
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkings", ex);
            }
        }
        #endregion
        
        #region ParkingContract
        public ParkingContract AddParkingContract(ParkingContract parkingContract)
        {
            if (parkingContract == null) throw new ParkingManagerException("ParkingManager - AddParkingContract - parkingContract is null.");
            try
            {
                return _repo.WriteParkingContractInDB(parkingContract);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - AddParkingContract", ex);
            }
        }

        public void RemoveParkingContract(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - RemoveParkingContract - id is invalid.");
            try
            {
                if (!_repo.ParkingContractExistsInDB(id)) throw new ParkingManagerException("ParkingManager - RemoveParkingContract - parkingContract does not exist in DB.");
                _repo.RemoveParkingContractFromDB(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - RemoveParkingContract", ex);
            }
        }

        public void UpdateParkingContract(ParkingContract parkingContract)
        {
            if (parkingContract == null) throw new ParkingManagerException("ParkingManager - UpdateParkingContract - parkingContract is null.");
            try
            {
                if (!_repo.ParkingContractExistsInDB(parkingContract.ID)) throw new ParkingManagerException("ParkingManager - UpdateParkingContract - parkingContract does not exist in DB.");
                if (_repo.GetParkingContractById(parkingContract.ID).Equals(parkingContract)) throw new ParkingManagerException("ParkingManager - UpdateParkingContract - parkingContract is not different");
                _repo.UpdateParkingContract(parkingContract);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - UpdateParkingContract", ex);
            }
        }

        public ParkingContract GetParkingContractById(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - GetParkingContractById - id is not valid");
            try
            {
                if (!_repo.ParkingContractExistsInDB(id)) throw new ParkingManagerException("ParkingManager - GetParkingContractById - parkingContract does not exist in DB.");
                return _repo.GetParkingContractById(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingContractById", ex);
            }
        }
        
        public IReadOnlyList<ParkingContract> GetParkingContracts()
        {
            try
            {
                return _repo.GetParkingContracts();
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingContracts", ex);
            }
        }

        public IReadOnlyList<ParkingContract> GetParkingContracts(int parkingId)
        {
            try
            {
                return _repo.GetParkingContracts(parkingId);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingContracts", ex);
            }
        }
        #endregion

        #region ParkingDetails
        public ParkingDetail AddParkingDetail(ParkingDetail parkingDetail)
        {
            if (parkingDetail == null) throw new ParkingManagerException("ParkingManager - AddParkingDetail - parkingDetail is null.");
            try
            {
                return _repo.WriteParkingDetailInDB(parkingDetail);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - AddParkingDetail", ex);
            }
        }

        public void RemoveParkingDetail(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - RemoveParkingDetail - id is invalid.");
            try
            {
                if (!_repo.ParkingDetailExistsInDB(id)) throw new ParkingManagerException("ParkingManager - RemoveParkingDetail - parkingDetail does not exist in DB.");
                _repo.RemoveParkingDetailFromDB(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - RemoveParkingDetail", ex);
            }
        }

        public void UpdateParkingDetail(ParkingDetail parkingDetail)
        {
            if (parkingDetail == null) throw new ParkingManagerException("ParkingManager - UpdateParkingDetail - parkingDetail is null.");
            try
            {
                if (!_repo.ParkingDetailExistsInDB(parkingDetail.ID)) throw new ParkingManagerException("ParkingManager - UpdateParkingDetail - parkingDetail does not exist in DB.");
                if (_repo.GetParkingDetailById(parkingDetail.ID).Equals(parkingDetail)) throw new ParkingManagerException("ParkingManager - UpdateParkingDetail - parkingDetail is not different");
                _repo.UpdateParkingDetail(parkingDetail);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - UpdateParkingDetail", ex);
            }
        }
        
        public ParkingDetail GetParkingDetail(int id)
        {
            if (id < 1) throw new ParkingManagerException("ParkingManager - GetParkingDetail - id is not valid");
            try
            {
                if (!_repo.ParkingDetailExistsInDB(id)) throw new ParkingManagerException("ParkingManager - GetParkingDetail - parkingDetail does not exist in DB.");
                return _repo.GetParkingDetailById(id);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingDetail", ex);
            }
        }

        public IReadOnlyList<ParkingDetail> GetParkingDetails()
        {
            try
            {
                return _repo.GetParkingDetails();
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingDetails", ex);
            }
        }

        public IReadOnlyList<ParkingDetail> GetParkingDetails(int parkingId)
        {
            try
            {
                return _repo.GetParkingDetails(parkingId);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - GetParkingDetails", ex);
            }
        }
        #endregion
    }
}
