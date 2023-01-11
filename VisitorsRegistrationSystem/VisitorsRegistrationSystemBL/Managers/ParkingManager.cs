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
        /// <summary>
        /// This method throws an exception if parking is null
        /// If no exception is thrown it adds the parking to the database.
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
        public Parking AddParking(Parking parking)
        {
            if (parking == null)
            {
                throw new ParkingManagerException("ParkingManager - AddParking - parking is null.");
            }
            try
            {
                return _repo.WriteParkingInDB(parking);
            }
            catch (Exception ex)
            {
                throw new ParkingManagerException("ParkingManager - AddParking", ex);
            }
        }
        /// <summary>
        /// This method throws an exception when id of a parking is smaller then 1 and if parking is not found by id in the database.
        /// If no exception is thrown this method removes a parking by id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if parking is null.
        /// This method also throws an exception if parking is not found by id in the database or if the parking is not changed.
        /// If no exception is thrown this method update the parking.
        /// </summary>
        /// <param name="parking"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the id of parking is smaller then 1 or if the parking is not found by id.
        /// If no exception is thrown the method returns a parking by id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method retruns a list of parkings from the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method returns a exception if parkingcontract is null.
        /// If no exception is thrown this method adds a parkingContract to the database.
        /// </summary>
        /// <param name="parkingContract"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the id of a parkingContract is smaller than 1 or if it's not found in the database.
        /// If no exception is thrown this method removes a parkingContract by id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if parkingContract is null.
        /// This method also throws an exception if parkingContract is not found by id in the database or if the parkingContract is not changed.
        /// If no exception is thrown the parkingContract is updated.
        /// </summary>
        /// <param name="parkingContract"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the id of a parkingContract is smaller then 1 or if it's not found in the database
        /// If no exception is thrown this method returns a parkingContract by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method returns a list of parkingContracts.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method returns a list of parkingContract by parkingId.
        /// </summary>
        /// <param name="parkingId"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if parkingDetail is null.
        /// If no exception is thrown this method adds a parkingDetail to the database.
        /// </summary>
        /// <param name="parkingDetail"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the id of a parkingDetail is smaller then 1 or if the id of the parkingDetail is not found in the database.
        /// If no exception is found this method removes a parkingDetail by id form the database.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if parkingDetail is null.
        /// This method also throws an exception if the parkingDetail is not found by id in the database or if the parkingDetail is not changed.
        /// If no exception is thrown this method will update the parkingDetail.
        /// </summary>
        /// <param name="parkingDetail"></param>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the id of parkingDetail is smealler then 1 or if the parkingDetail is not found by id in the database.
        /// If no exception is thrown this method returns a parkingDetail by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method returns a list of parkingDetails.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
        /// <summary>
        /// This method returns a list of parkingDetails by parkingId.
        /// </summary>
        /// <param name="parkingId"></param>
        /// <returns></returns>
        /// <exception cref="ParkingManagerException"></exception>
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
