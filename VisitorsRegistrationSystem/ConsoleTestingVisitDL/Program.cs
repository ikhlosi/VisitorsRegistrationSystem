using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!\n");
// !!! vergeet connectionstring niet aan te passen !!!
DotNetEnv.Env.TraversePath().Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
VisitRepositoryADO vRepo = new VisitRepositoryADO(connectionString);

Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester",null);
Address address = new Address("Elsegem","9790", "Kouterlos", "2a", null);
Company company = CompanyFactory.MakeCompany(1, "Brightest", "1234567890", address, "+32479564643", "brightest@bright.com");
Visitor visitor = VisitorFactory.MakeVisitor(1, "Arno", "arnovantieghem@gmail.com", "Brightest");
DateTime startTime = new DateTime(2023, 12, 29);
Visit visit = VisitFactory.MakeVisit(null,visitor, company, employee);

//foreach (var i in vRepo.GetVisits())
//{
//    Console.WriteLine("\t" + i.ToString());
//}
//Console.WriteLine("Adding visit");
//vRepo.AddVisit(visit);
//Console.WriteLine("Visit has been added");

//Console.WriteLine("Removing visit with id 3");
//vRepo.RemoveVisit(3);
//Console.WriteLine("Visit with id 3 has been deleted");

//DateTime endTime = new DateTime(2023, 12, 31);
//Visit visitUpdate = VisitFactory.MakeVisit(2, visitor, company, employee, startTime);

////Console.WriteLine("Updating visit with id 2");
////vRepo.UpdateVisit(visitUpdate);
////Console.WriteLine("Visit with id 2 has been updated");

//Console.WriteLine("Bestaat Visit met id: 1 (moet true zijn)" + vRepo.VisitExists(1));
//Console.WriteLine("Bestaat Visit met id: 9999 (moet false zijn)" + vRepo.VisitExists(9999));

//Console.WriteLine("Bestaat Visit met VisitorId: 2 en startDate: 30-12-2023 (moet true zijn)" + vRepo.VisitExists(visitUpdate));
//Visitor visitor1 = VisitorFactory.MakeVisitor(3, "Arno", "arnov@gmail.com", "Brightest");
//Visit visitUpdate1 = VisitFactory.MakeVisit(2, visitor1, company, employee, startTime);
//Console.WriteLine("Bestaat Visit met VisitorId: 3 en startDate: 30-12-2023 (moet false zijn)" + vRepo.VisitExists(visitUpdate1));
Visit visit1 = vRepo.GetVisit(4);
Console.WriteLine("Geef Visit met id: 4" + visit1);