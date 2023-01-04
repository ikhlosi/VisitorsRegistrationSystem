// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!");

DotNetEnv.Env.TraversePath().Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
Console.WriteLine(connectionString); // for debug
ParkingRepositoryADO pRepo = new ParkingRepositoryADO(connectionString);


// done
Parking parking = pRepo.GetParkingById(1);
// done
Parking parking1 = ParkingFactory.MakeParking(1,0, false, null, null, 100);
parking1 = pRepo.WriteParkingInDB(parking1);
Console.WriteLine(parking1);

// done
bool result = pRepo.ParkingExistsInDB(parking1.ID);
Console.WriteLine(result);
result = pRepo.ParkingExistsInDB(999);
Console.WriteLine(result);

// done
pRepo.RemoveParkingFromDB(parking1.ID);

// done
parking.SetTotalSpaces(200);
pRepo.UpdateParking(parking); // parking met id 1

// done
IReadOnlyList<ParkingDTO> parkingDTOs = pRepo.GetParkings();
foreach (ParkingDTO parkingDTO in parkingDTOs)
{
    Console.WriteLine(parkingDTO);
}

// done
Address address = new Address(1, "Teststraat","9000", "1", "1000", "Brussel");
Company company = CompanyFactory.MakeCompany(1, "TestCompany", "BE1234567890", address, "0479564643", "test@test.com");
ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(null,company, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), 10, 1);
pRepo.WriteParkingContractInDB(parkingContract);

// done
parkingContract = pRepo.GetParkingContractById(1);
Console.WriteLine(parkingContract);

// done
IReadOnlyList<ParkingContract> parkingContracts = pRepo.GetParkingContracts();
foreach (ParkingContract parkingContract1 in parkingContracts)
{
    Console.WriteLine(parkingContract1);
}

// done
Console.WriteLine(pRepo.ParkingContractExistsInDB(1));
Console.WriteLine(pRepo.ParkingContractExistsInDB(9999));

// done
parkingContract.SetEndDate(DateTime.Now.AddYears(2));
pRepo.UpdateParkingContract(parkingContract);

// done
Console.WriteLine(pRepo.ParkingContractExistsInDB(1));
Console.WriteLine(pRepo.ParkingContractExistsInDB(9999));

// done
pRepo.RemoveParkingDetailFromDB(1);

// done
Console.WriteLine(pRepo.GetParkingDetailById(2));

// done
ParkingDetail parkingDetail = pRepo.GetParkingDetailById(2);
parkingDetail.SetLicensePlate("1-ABC-123");
pRepo.UpdateParkingDetail(parkingDetail);