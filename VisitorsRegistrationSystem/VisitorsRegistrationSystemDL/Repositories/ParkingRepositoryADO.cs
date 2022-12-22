using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class ParkingRepositoryADO : IParkingRepository
    {

        private string connectionString;

        public ParkingRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // parking methodes DONE + checked
        #region Parking
        public Parking GetParkingById(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select p.id as pId, p.totalSpaces as pTs, p.occupiedSpaces as pOs, p.full as pFu, pc.id as pcId, pc.companyId as pcCo, pc.spaces as pcSp, pc.startDate as pcSd, pc.endDate as pcEd, pd.id as pdId, pd.startTime as pdSt, pd.endTime as pdEt, pd.licensePlate as pdLi, pd.visitedCompanyId as pdVi, c.id as cId, c.name as cNa, c.vat as cVa, c.email as cEm, c.telNr as cTe, a.id as aId, a.street as aSt, a.city as aCi,a.postalCode as aPo, a.houseNr as aHo, a.bus as aBu from parking p inner join parkingcontract pc on p.id = pc.parkingId inner join parkingdetails pd on pd.parkingId = p.id join company c on pc.companyId = c.id join address a on c.addressId = a.id where p.id= 1 and p.visible=1 and pc.visible = 1 and pd.visible = 1 and c.visible = 1 and a.visible = 1;";
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
                    int parkingDetailVisitedCompanyId = 0;

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
                        // loop other times
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
                            parkingDetailVisitedCompanyId = (int)reader["pdVi"];
                            parkingDetail = new ParkingDetail(parkingDetailId, startTime, endTime, licensePlate, parkingDetailVisitedCompanyId, parkingId);
                            parkingDetails.Add(parkingDetail);
                        }

                    }
                    // Value returnen
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

        public Parking WriteParkingInDB(Parking parking)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into parking (totalSpaces,occupiedSpaces,full) values(@totalSpaces,@occupiedSpaces,@full)";
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

        public void RemoveParkingFromDB(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parking set visible=0 where id = @id and visible=1";
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

        public void UpdateParking(Parking parking)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parking set totalSpaces = @totalSpaces, occupiedSpaces = @occupiedSpaces, full = @full where id = @id;";
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

        public IReadOnlyList<ParkingDTO> GetParkings()
        {
            List<ParkingDTO> parkings = new List<ParkingDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from parking where visible = 1 order by id;";
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

        // parkingContract methodes DONE + checked
        #region ParkingContract
        public ParkingContract WriteParkingContractInDB(ParkingContract parkingContract)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into parkingcontract(companyId, spaces,startDate,endDate,parkingId) values(@companyId,@spaces,@startDate,@endDate,@parkingId)";
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

        public ParkingContract GetParkingContractById(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select pc.id as pcId, pc.companyId as pcCi, pc.spaces as pcSp, pc.startDate as pcSt, pc.endDate as pcEn, pc.parkingId as pcPa, c.name as cNa, c.VAT as cVa, c.email as cEm, c.telNr as cTe, c.addressId as cAd, a.street as aSt, a.city as aCi,a.postalCode as aPo, a.houseNr as aHo, a.bus as aBu from parkingcontract pc join company c on pc.companyId = c.id join address a on c.addressId = a.id where pc.id= @id and pc.visible = 1;";
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

        public IReadOnlyList<ParkingContractDTO> GetParkingContracts()
        {
            List<ParkingContractDTO> parkingContracts = new List<ParkingContractDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from parkingContract where visible = 1 order by id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        parkingContracts.Add(new ParkingContractDTO((int)reader["id"], (int)reader["companyId"], (int)reader["spaces"], (DateTime)reader["startDate"], (DateTime)reader["endDate"], (int)reader["parkingId"]));
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

        public bool ParkingContractExistsInDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM parkingContract WHERE id=@id and visible = 1";
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

        public void RemoveParkingContractFromDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parkingContract set visible=0 where id = @id and visible=1";
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

        public void UpdateParkingContract(ParkingContract parkingContract)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parkingcontract set companyId = @companyId, spaces=@spaces,startDate = @startDate,endDate = @endDate, parkingId=@parkingId where id=@id;";
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

        public bool ParkingDetailExistsInDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM parkingdetails WHERE id=@id and visible = 1";
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

        public void RemoveParkingDetailFromDB(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parkingDetails set visible=0 where id = @id and visible=1";
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

        public void UpdateParkingDetail(ParkingDetail parkingDetail)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update parkingdetails set startTime = @startTime, endTime= @endTime, licensePlate= @licensePlate,visitedCompanyId= @visitedCompanyId,parkingId= @parkingId where id=@id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@startTime", parkingDetail.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", parkingDetail.EndTime);
                    cmd.Parameters.AddWithValue("@licensePlate", parkingDetail.LicensePlate);
                    cmd.Parameters.AddWithValue("@visitedCompanyId", parkingDetail.VisitedCompanyID);
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

        public ParkingDetail GetParkingDetailById(int iD)
        {

            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT * FROM parkingdetails WHERE id=@id and visible = 1";
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
                        int ID = reader.GetInt32("id");
                        DateTime StartTime = reader.GetDateTime("startTime");
                        DateTime EndTime = reader.GetDateTime("endTime");
                        string LicensePlate = reader.GetString("licensePlate");
                        int VisitedCompanyID = reader.GetInt32("visitedCompanyId");
                        int ParkingId = reader.GetInt32("parkingId");
                        parkingDetail = new ParkingDetail(ID, StartTime, EndTime, LicensePlate, VisitedCompanyID, ParkingId);
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
        public IReadOnlyList<ParkingDetailDTO> GetParkingDetails()
        {
            List<ParkingDetailDTO> parkingDetails = new List<ParkingDetailDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from parkingDetails where visible = 1 order by id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        parkingDetails.Add(new ParkingDetailDTO((int)reader["id"], (DateTime)reader["startTime"], (DateTime)reader["endTime"], (string)reader["licensePlate"], (int)reader["visitedCompanyId"], (int)reader["parkingId"]));
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
        
        public ParkingDetail WriteParkingDetailInDB(ParkingDetail parkingDetail)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into parkingdetails(startTime,endTime,licensePlate,visitedCompanyId,parkingId) values (@startTime,@endTime,@licensePlate,@visitedCompanyId,@parkingId);";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@startTime", parkingDetail.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", parkingDetail.EndTime);
                    cmd.Parameters.AddWithValue("@licensePlate", parkingDetail.LicensePlate);
                    cmd.Parameters.AddWithValue("@visitedCompanyId", parkingDetail.VisitedCompanyID);
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

        public IReadOnlyList<ParkingContractDTO> GetParkingContracts(int parkingId)
        {
            List<ParkingContractDTO> parkingContracts = new List<ParkingContractDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from parkingContract where visible = 1 and parkingId=@parkingId order by id;";
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
                        parkingContracts.Add(new ParkingContractDTO((int)reader["id"], (int)reader["companyId"], (int)reader["spaces"], (DateTime)reader["startDate"], (DateTime)reader["endDate"], (int)reader["parkingId"]));
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


        public IReadOnlyList<ParkingDetailDTO> GetParkingDetails(int parkingId)
        {
            List<ParkingDetailDTO> parkingDetails = new List<ParkingDetailDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from parkingDetails where visible = 1 and parkingId=@parkingId order by id;";
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
                        parkingDetails.Add(new ParkingDetailDTO((int)reader["id"], (DateTime)reader["startTime"], (DateTime)reader["endTime"], (string)reader["licensePlate"], (int)reader["visitedCompanyId"], (int)reader["parkingId"]));
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
