using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!\n");
// !!! vergeet connectionstring niet aan te passen !!!
string connectionStringArno = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
VisitRepositoryADO vRepo = new VisitRepositoryADO(connectionStringArno);

Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
Address address = new Address("Elsegem", "Kouterlos", "2a", null);
Company company = CompanyFactory.MakeCompany(1, "Brightest", "1234567890", address, "+32479564643", "brightest@bright.com");
Visitor visitor = VisitorFactory.MakeVisitor(1, "Arno", "arnovantieghem@gmail.com", "Brightest");
Visit visit = new Visit(visitor, company, employee, DateTime.MaxValue, DateTime.MaxValue);

//Console.WriteLine("Adding visit");
//vRepo.AddVisit(visit);
//Console.WriteLine("Visit has been added");

//Console.WriteLine("Removing visit with id 14");
//vRepo.RemoveVisit(14);
//Console.WriteLine("Visit with id 14 has been deleted");

DateTime startTime = new DateTime(2023, 12, 30);
DateTime endTime = new DateTime(2023, 12, 31);
Visit visitUpdate = new Visit(2, visitor, company, employee, startTime, endTime);

//Console.WriteLine("Updating visit with id 2");
//vRepo.UpdateVisit(visitUpdate);
//Console.WriteLine("Visit with id 2 has been updated");

Console.WriteLine("Bestaat Visit met id: 1 (moet true zijn)" + vRepo.VisitExists(1));
Console.WriteLine("Bestaat Visit met id: 9999 (moet false zijn)" + vRepo.VisitExists(9999));

Console.WriteLine("Bestaat Visit met VisitorId: 2 en startDate: 30-12-2023 (moet true zijn)" + vRepo.VisitExists(visitUpdate));
Visitor visitor1 = VisitorFactory.MakeVisitor(3, "Arno", "arnov@gmail.com", "Brightest");
visitUpdate.VisitorSet(visitor1);
Console.WriteLine("Bestaat Visit met VisitorId: 3 en startDate: 30-12-2023 (moet false zijn)" + vRepo.VisitExists(visitUpdate));

Console.WriteLine("Geef Visit met id: 1" + vRepo.GetVisit(1).ToString());