// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!");

DotNetEnv.Env.TraversePath().Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
Console.WriteLine(connectionString); // for debug
ParkingRepositoryADO pRepo = new ParkingRepositoryADO(connectionString);


// done
Parking parking = pRepo.GetParkingById(1);
// done
Parking parking1 = new Parking(0, false, null, null, 100);
parking1 = pRepo.WriteParkingInDB(parking1);
Console.WriteLine(parking1);

// done
bool result = pRepo.ParkingExistsInDB(1);
Console.WriteLine(result);
result = pRepo.ParkingExistsInDB(999);
Console.WriteLine(result);