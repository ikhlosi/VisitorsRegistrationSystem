using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;
using VisitorsRegistrationSystemDL.Repositories;

string connString = "Data Source=LAPTOP-GGBV7H48\\SQLEXPRESS;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
IVisitorRepository repo = new VisitorRepositoryADO(connString);
VisitorManager vm = new VisitorManager(repo);

vm.AddVisitor(VisitorFactory.MakeVisitor(null, "Ben", "ben@ben.com", ""));

Visitor testVisitor = vm.GetVisitors().Last();
int visitorId = testVisitor.Id;
Console.WriteLine(testVisitor + " => (ADD)");
Console.WriteLine("Count: " + vm.GetVisitors().Count);

vm.UpdateVisitor(VisitorFactory.MakeVisitor(visitorId,"Jan", "jan@jan.be", "JanBiZZ"));

Visitor testVisitor2 = vm.GetVisitors().Last();
Console.WriteLine(testVisitor2 + " => (UPDATE)");
Console.WriteLine("Count: " + vm.GetVisitors().Count);

vm.DeleteVisitor(testVisitor2);
Console.WriteLine(testVisitor2 + " => (DELETE)");
Console.WriteLine("Count: " + vm.GetVisitors().Count);
 