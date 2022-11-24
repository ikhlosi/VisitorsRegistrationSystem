using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!\n");
// !!! vergeet connectionstring niet aan te passen !!!
string connectionStringArno = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
VisitRepositoryADO vRepo = new VisitRepositoryADO(connectionStringArno);

foreach (var i in vRepo.GetAllVisitors())
{
    Console.WriteLine("\t" + i.ToString());
}
