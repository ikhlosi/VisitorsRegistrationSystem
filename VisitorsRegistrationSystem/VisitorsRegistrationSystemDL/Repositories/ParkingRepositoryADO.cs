using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;

namespace VisitorsRegistrationSystemDL.Repositories
{
    /// <summary>
    /// This is the class responsible for sending queries pertaining
    /// the management of parkings in the database.
    /// It implements the IParkingRepository interface.
    /// </summary>
    public class ParkingRepositoryADO : IParkingRepository
    {

        private string connectionString;

        public ParkingRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Parking
        /// <summary>
        /// This method retrieves a parking object given its ID.
        /// </summary>
        /// <param name="iD">The ID of the parking to retrieve.</param>
        /// <returns>A parking object matching the given ID.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Parking GetParkingById(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select p.id as pId, p.totalSpaces as pTs, p.occupiedSpaces as pOs, p.full as pFu, pc.id as pcId, pc.companyId as pcCo, pc.spaces as pcSp, pc.startDate as pcSd, pc.endDate as pcEd, pd.id as pdId, pd.startTime as pdSt, pd.endTime as pdEt, pd.licensePlate as pdLi, pd.visitedCompanyId as pdVi, c.id as cId, c.name as cNa, c.vat as cVa, c.email as cEm, c.telNr as cTe, a.id as aId, a.street as aSt, a.city as aCi,a.postalCode as aPo, a.houseNr as aHo, a.bus as aBu from Parking p inner join Parkingcontract pc on p.id = pc.parkingId inner join Parkingdetails pd on pd.parkingId = p.id join Company c on pc.companyId = c.id join Address a on c.addressId = a.id where p.id= 1 and p.visible=1 and pc.visible = 1 and pd.visible = 1 and c.visible = 1 and a.visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);

                    int parkingId = 0;
                    int totalSpaces = 0;
                    int occupiedSpaces = 0;
                    bool full = false;

                    int parkingContractId = 0;
                    int parkingContractCompanyId = 0;
                    int contractSpaces = 0;
                    DateTime startDate = DateTime.Now;
                    DateTime endDate = DateTime.Now;

                    int parkingDetailId = 0;
                    DateTime startTime = DateTime.Now;
                    DateTime endTime = DateTime.Now;
                    string licensePlate = "";

                    int companyId = 0;
                    string companyName = "";
                    string VAT = "";
                    string email = "";
                    string telNumber = "";

                    int addressId = 0;
                    string city = "";
                    string postalCode = "";
                    string street = "";
                    string houseNo = "";
                    string? busNo = "";

                    bool firstLoop = true;
                    List<int> passedContractIds = new List<int>();
                    List<int> passedDetailIds = new List<int>();
                    Parking parking = null;
                    ParkingContract parkingContract = null;
                    ParkingDetail parkingDetail = null;
                    Company company = null;
                    Address address = null;
                    List<ParkingContract> parkingContracts = new List<ParkingContract>();
                    List<ParkingDetail> parkingDetails = new List<ParkingDetail>();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (firstLoop)
                        {

                            parkingId = (int)reader["pId"];
                            totalSpaces = (int)reader["pTs"];
                            occupiedSpaces = (int)reader["pOs"];
                            full = Convert.ToBoolean(reader["pFu"]);
                            firstLoop = false;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("pcId")) && !passedContractIds.Contains((int)reader["pcId"]))
                        {
                            parkingContractId = (int)reader["pcId"];
                            passedContractIds.Add(parkingContractId);
                            parkingContractCompanyId = (int)reader["pcCo"];
                            contractSpaces = (int)reader["pcSp"];
                            startDate = (DateTime)reader["pcSd"];
                            endDate = (DateTime)reader["pcEd"];

                            companyId = (int)reader["cId"];
                            companyName = (string)reader["cNa"];
                            VAT = (string)reader["cVa"];
                            email = (string)reader["cEm"];
                            telNumber = (string)reader["cTe"];

                            addressId = (int)reader["aId"];
                            city = (string)reader["aSt"];
                            postalCode = (string)reader["aPo"];
                            street = (string)reader["aCi"];
                            houseNo = (string)reader["aHo"];
                            if (reader["aBu"] != DBNull.Value)
                            {
                                busNo = (string)reader["aBu"];
                            }

                            if (string.IsNullOrEmpty(busNo)) address = new Address(addressId, city, postalCode, street, houseNo, null);
                            else address = new Address(addressId, city, postalCode, street, houseNo, busNo);
                            company = CompanyFactory.MakeCompany(companyId, companyName, VAT, address, telNumber, email);
                            parkingContract = new ParkingContract(parkingContractId, company, startDate, endDate, contractSpaces, parkingId);
                            parkingContracts.Add(parkingContract);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("pdId")) && !passedDetailIds.Contains((int)reader["pdId"]))
                        {
                            parkingDetailId = (int)reader["pdId"];
                            passedDetailIds.Add(parkingDetailId);
                            startTime = (DateTime)reader["pdSt"];
                            endTime = (DateTime)reader["pdEt"];
                            licensePlate = (string)reader["pdLi"];
                            parkingDetail = new ParkingDetail(parkingDetailId, startTime, endTime, licensePlate, company, parkingId);
                            parkingDetails.Add(parkingDetail);
                        }

                    }
                    parking = ParkingFactory.MakeParking(parkingId, occupiedSpaces, full, parkingContracts, parkingDetails, totalSpaces);
                    return parking;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkingById", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method inserts a parking object in the MySQL database
        /// and then sets the ID property of the inserted parking object.
        /// </summary>
        /// <param name="parking">The parking object to insert.</param>
        /// <returns>The inserted parking object with its ID property assigned.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Parking WriteParkingInDB(Parking parking)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into Parking (totalSpaces,occupiedSpaces,full) values(@totalSpaces,@occupiedSpaces,@full)";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@totalSpaces", parking.TotalSpaces);
                    cmd.Parameters.AddWithValue("@full", parking.Full);
                    cmd.Parameters.AddWithValue("@occupiedSpaces", parking.OccupiedSpaces);
                    cmd.ExecuteNonQuery();
                    int insertedId = (int)cmd.LastInsertedId;
                    parking.SetID(insertedId);
                    return parking;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("WriteParkingInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain parking object exists in the database.
        /// </summary>
        /// <param name="parkingID">The ID of the parking to check.</param>
        /// <returns>A bool indicitating whether the parking exists in the database or not.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool ParkingExistsInDB(int parkingID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Parking WHERE id=@id and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", parkingID);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("ParkingExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method removes a parking from the database by setting
        /// its visible column to 0.
        /// </summary>
        /// <param name="iD">The ID of the parking to remove.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void RemoveParkingFromDB(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parking set visible=0 where id = @id and visible=1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("RemoveParkingFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method updates a company in the MySQL database.
        /// </summary>
        /// <param name="parking">The parking object with its updated properties.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void UpdateParking(Parking parking)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parking set totalSpaces = @totalSpaces, occupiedSpaces = @occupiedSpaces, full = @full where id = @id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", parking.ID);
                    cmd.Parameters.AddWithValue("@totalSpaces", parking.TotalSpaces);
                    cmd.Parameters.AddWithValue("@occupiedSpaces", parking.OccupiedSpaces);
                    cmd.Parameters.AddWithValue("@full", parking.Full);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("UpdateParking", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves all parkings from the database.
        /// </summary>
        /// <returns>A readonly list of all the parkings in the database.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<ParkingDTO> GetParkings()
        {
            List<ParkingDTO> parkings = new List<ParkingDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from Parking where visible = 1 order by id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        parkings.Add(new ParkingDTO((int)reader["id"], (int)reader["totalSpaces"], (int)reader["occupiedSpaces"]));
                    }
                    reader.Close();
                    return parkings;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkings");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region ParkingContract
        /// <summary>
        /// This method inserts a parkingcontract object in the MySQL database
        /// and then sets its ID property from the ID which the database assigns.
        /// </summary>
        /// <param name="parkingContract">The parkingcontract object to insert.</param>
        /// <returns>The inserted parkingcontract object with its assigned ID.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public ParkingContract WriteParkingContractInDB(ParkingContract parkingContract)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into Parkingcontract(companyId, spaces,startDate,endDate,parkingId) values(@companyId,@spaces,@startDate,@endDate,@parkingId)";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", parkingContract.Company.ID);
                    cmd.Parameters.AddWithValue("@spaces", parkingContract.ReservedSpace);
                    cmd.Parameters.AddWithValue("@startDate", parkingContract.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", parkingContract.EndDate);
                    cmd.Parameters.AddWithValue("@parkingId", parkingContract.parkingId);
                    cmd.ExecuteNonQuery();
                    int insertedId = (int)cmd.LastInsertedId;
                    parkingContract.SetID(insertedId);
                    return parkingContract;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("WriteParkingContractInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves a parkingcontract object from the database.
        /// </summary>
        /// <param name="iD">The ID of the parkingcontract object to retrieve.</param>
        /// <returns>A parkingcontract object matching the ID.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public ParkingContract GetParkingContractById(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select pc.id as pcId, pc.companyId as pcCi, pc.spaces as pcSp, pc.startDate as pcSt, pc.endDate as pcEn, pc.parkingId as pcPa, c.name as cNa, c.VAT as cVa, c.email as cEm, c.telNr as cTe, c.addressId as cAd, a.street as aSt, a.city as aCi,a.postalCode as aPo, a.houseNr as aHo, a.bus as aBu from Parkingcontract pc join Company c on pc.companyId = c.id join Address a on c.addressId = a.id where pc.id= @id and pc.visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    string busNr = "";
                    if (reader["aBu"] != DBNull.Value)
                    {
                        busNr = (string)reader["aBu"];
                    }
                    Address address = new Address((string)reader["aCi"], (string)reader["aPo"], (string)reader["aSt"], (string)reader["aHo"], busNr);
                    Company company = CompanyFactory.MakeCompany((int)reader["pcCi"], (string)reader["cNa"], (string)reader["cVa"], address, (string)reader["cTe"], (string)reader["cEm"]);
                    ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract((int)reader["pcId"], company, (DateTime)reader["pcSt"], (DateTime)reader["pcEn"], (int)reader["pcSp"], (int)reader["pcPa"]);
                    reader.Close();
                    return parkingContract;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisitor");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves all the parkingcontract objects from the database.
        /// </summary>
        /// <returns>A readonly list of all the parkingcontract objects from the database.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<ParkingContract> GetParkingContracts()
        {
            List<ParkingContract> parkingContracts = new List<ParkingContract>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select *, pc.id as pcId, c.id as cId from Parkingcontract pc join Company c on pc.companyId = c.id join Address a on c.addressId = a.id where pc.visible = 1 and c.visible = 1 order by pc.id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string busnummer = null;
                        if (reader["bus"] != DBNull.Value)
                        {
                            busnummer = (string)reader["bus"];
                        }
                        Address address = new Address((int)reader["addressid"],(string)reader["city"], (string)reader["postalcode"], (string)reader["street"], (string)reader["houseNr"], busnummer);
                        Company company = CompanyFactory.MakeCompany((int)reader["cId"], (string)reader["name"], (string)reader["VAT"], address, (string)reader["telNr"], (string)reader["email"]);
                        parkingContracts.Add(ParkingContractFactory.MakeParkingContract((int)reader["id"], company, (DateTime)reader["startDate"], (DateTime)reader["endDate"], (int)reader["spaces"], (int)reader["parkingId"]));
                    }
                    reader.Close();
                    return parkingContracts;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkings");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain parkingcontract object exists in the database.
        /// </summary>
        /// <param name="id">The ID of the parkingcontract object to check.</param>
        /// <returns>
        /// A bool indicating whether the parkingcontract
        /// object exists in the database or not
        /// </returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool ParkingContractExistsInDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Parkingcontract WHERE id=@id and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("ParkingContractExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method removes a certain parkingcontract object from the database.
        /// </summary>
        /// <param name="id">The ID of the parkingcontract object to remove.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void RemoveParkingContractFromDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parkingcontract set visible=0 where id = @id and visible=1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("RemoveParkingContractFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method updates a parkingcontract object in the MySQL database.
        /// </summary>
        /// <param name="parkingContract">The updated parkingcontract object.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void UpdateParkingContract(ParkingContract parkingContract)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parkingcontract set companyId = @companyId, spaces=@spaces,startDate = @startDate,endDate = @endDate, parkingId=@parkingId where id=@id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", parkingContract.Company.ID);
                    cmd.Parameters.AddWithValue("@spaces", parkingContract.ReservedSpace);
                    cmd.Parameters.AddWithValue("@startDate", parkingContract.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", parkingContract.EndDate);
                    cmd.Parameters.AddWithValue("@parkingId", parkingContract.parkingId);
                    cmd.Parameters.AddWithValue("@id", parkingContract.ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("UpdateParkingContract", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion


        #region ParkingDetail

        /// <summary>
        /// This method checks whether a certain parkingdetail object exists in the database.
        /// </summary>
        /// <param name="id">The ID of the parkingdetail object to check for.</param>
        /// <returns>A bool indicating whether the parkingdetail object exists in the database or not.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool ParkingDetailExistsInDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Parkingdetails WHERE id=@id and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("ParkingDetailExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method removes a certain parkingdetail object from the MySQL database.
        /// </summary>
        /// <param name="id">The ID of the parkingdetail object to be removed.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void RemoveParkingDetailFromDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parkingdetails set visible=0 where id = @id and visible=1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("RemoveParkingDetailFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method updates the parkingdetail object in the MySQL database.
        /// </summary>
        /// <param name="parkingDetail">The updated parkingdetail object.</param>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void UpdateParkingDetail(ParkingDetail parkingDetail)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Parkingdetails set startTime = @startTime, endTime= @endTime, licensePlate= @licensePlate,visitedCompanyId= @visitedCompanyId,parkingId= @parkingId where id=@id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@startTime", parkingDetail.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", parkingDetail.EndTime);
                    cmd.Parameters.AddWithValue("@licensePlate", parkingDetail.LicensePlate);
                    cmd.Parameters.AddWithValue("@visitedCompanyId", parkingDetail.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@parkingId", parkingDetail.ParkingId);
                    cmd.Parameters.AddWithValue("@id", parkingDetail.ID);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("UpdateParkingDetail", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves a parkingdetail object from the database given its ID.
        /// </summary>
        /// <param name="iD">The ID of the parkingdetail object to retrieve.</param>
        /// <returns></returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public ParkingDetail GetParkingDetailById(int iD)
        {

            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT *, pd.id as pdId, c.id as cId FROM Parkingdetails pd join Company c on pd.visitedCompanyId = c.id join Address a on c.addressId = a.id having pdId = @id and pd.visible = 1 and c.visible = 1 and a.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    ParkingDetail parkingDetail = null;
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32("pdId");
                        DateTime StartTime = reader.GetDateTime("startTime");
                        DateTime EndTime = reader.GetDateTime("endTime");
                        string LicensePlate = reader.GetString("licensePlate");
                        string busnummer = null;
                        if (reader["bus"] != DBNull.Value)
                        {
                            busnummer = (string)reader["bus"];
                        }
                        Address address = new Address((int)reader["addressid"], (string)reader["city"], (string)reader["postalcode"], (string)reader["street"], (string)reader["houseNr"], busnummer);
                        Company company = CompanyFactory.MakeCompany((int)reader["cId"], (string)reader["name"], (string)reader["VAT"], address, (string)reader["telNr"], (string)reader["email"]); int ParkingId = reader.GetInt32("parkingId");
                        parkingDetail = new ParkingDetail(ID, StartTime, EndTime, LicensePlate, company, ParkingId);
                    }
                    return parkingDetail;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkingDetailById", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// This method retrieves all parkingdetail objects from the database.
        /// </summary>
        /// <returns>
        /// A readonly list containing all the parkingdetail objects
        /// from the database.
        /// </returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<ParkingDetail> GetParkingDetails()
        {
            List<ParkingDetail> parkingDetails = new List<ParkingDetail>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select *,c.id as cId, pd.id as pdId from Parkingdetails pd join Company c on pd.visitedCompanyId = c.id join Address a on a.id = c.addressid where pd.visible = 1 and c.visible = 1 and a.visible = 1 order by pd.id";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string busnummer = null;
                        if (reader["bus"] != DBNull.Value)
                        {
                            busnummer = (string)reader["bus"];
                        }
                        Address address = new Address((int)reader["addressid"], (string)reader["city"], (string)reader["postalcode"], (string)reader["street"], (string)reader["houseNr"], busnummer);
                        Company company = CompanyFactory.MakeCompany((int)reader["cId"], (string)reader["name"], (string)reader["VAT"], address, (string)reader["telNr"], (string)reader["email"]);
                        parkingDetails.Add(ParkingDetailFactory.MakeParkingDetail((int)reader["pdId"], (DateTime)reader["startTime"], (DateTime)reader["endTime"], (string)reader["licensePlate"], company, (int)reader["parkingId"]));
                    }
                    reader.Close();
                    return parkingDetails;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkingDetails");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        /// <summary>
        /// This method inserts a parkingdetail object in the MySQL database 
        /// and then sets its ID property from the ID assigned by the database.
        /// </summary>
        /// <param name="parkingDetail">The parkingdetail object to insert.</param>
        /// <returns>The parkingdetail object with its assigned ID.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public ParkingDetail WriteParkingDetailInDB(ParkingDetail parkingDetail)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into Parkingdetails(startTime,endTime,licensePlate,visitedCompanyId,parkingId) values (@startTime,@endTime,@licensePlate,@visitedCompanyId,@parkingId);";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@startTime", parkingDetail.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", parkingDetail.EndTime);
                    cmd.Parameters.AddWithValue("@licensePlate", parkingDetail.LicensePlate);
                    cmd.Parameters.AddWithValue("@visitedCompanyId", parkingDetail.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@parkingId", parkingDetail.ParkingId);
                    cmd.ExecuteNonQuery();
                    int insertedId = (int)cmd.LastInsertedId;
                    parkingDetail.SetID(insertedId);
                    return parkingDetail;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("WriteParkingDetailInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves all parking contracts connected to a certain parking.
        /// </summary>
        /// <param name="parkingId">
        /// The ID of the parking for which the parking contracts should be retrieved.
        /// </param>
        /// <returns>A readonly list of the parking contracts matching the parking ID.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<ParkingContract> GetParkingContracts(int parkingId)
        {
            List<ParkingContract> parkingContracts = new List<ParkingContract>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select *, pc.id as pcId, c.id as cId from Parkingcontract pc join Company c on pc.companyId = c.id join Address a on c.addressId = a.id where pc.parkingId=@parkingId and pc.visible = 1 and c.visible = 1 order by pc.id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@parkingId", parkingId);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string busnummer = null;
                        if (reader["bus"] != DBNull.Value)
                        {
                            busnummer = (string)reader["bus"];
                        }
                        Address address = new Address((int)reader["addressid"], (string)reader["city"], (string)reader["postalcode"], (string)reader["street"], (string)reader["houseNr"], busnummer);
                        Company company = CompanyFactory.MakeCompany((int)reader["cId"], (string)reader["name"], (string)reader["VAT"], address, (string)reader["telNr"], (string)reader["email"]);
                        parkingContracts.Add(ParkingContractFactory.MakeParkingContract((int)reader["id"], company, (DateTime)reader["startDate"], (DateTime)reader["endDate"], (int)reader["spaces"], (int)reader["parkingId"]));
                    }
                    reader.Close();
                    return parkingContracts;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkings");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// This method retrieves the parking details of a certain parking.
        /// </summary>
        /// <param name="parkingId">
        /// The ID of the parking for which the parking details should be retrieved.
        /// </param>
        /// <returns>A readonly list of the parking details matching the parking.</returns>
        /// <exception cref="ParkingRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<ParkingDetail> GetParkingDetails(int parkingId)
        {
            List<ParkingDetail> parkingDetails = new List<ParkingDetail>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select *,c.id as cId, pd.id as pdId from Parkingdetails pd join Company c on pd.visitedCompanyId = c.id join Address a on a.id = c.addressid where pd.visible = 1 and c.visible = 1 and a.visible = 1 and pd.parkingId = @parkingId order by pd.id";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@parkingId", parkingId);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string busnummer = null;
                        if (reader["bus"] != DBNull.Value)
                        {
                            busnummer = (string)reader["bus"];
                        }
                        Address address = new Address((int)reader["addressid"], (string)reader["city"], (string)reader["postalcode"], (string)reader["street"], (string)reader["houseNr"], busnummer);
                        Company company = CompanyFactory.MakeCompany((int)reader["cId"], (string)reader["name"], (string)reader["VAT"], address, (string)reader["telNr"], (string)reader["email"]);
                        parkingDetails.Add(ParkingDetailFactory.MakeParkingDetail((int)reader["pdId"], (DateTime)reader["startTime"], (DateTime)reader["endTime"], (string)reader["licensePlate"], company, (int)reader["parkingId"]));
                    }
                    reader.Close();
                    return parkingDetails;
                }
                catch (Exception ex)
                {
                    throw new ParkingRepositoryADOException("GetParkingDetails");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
