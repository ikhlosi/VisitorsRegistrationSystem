using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;

namespace TestManagers
{
    public class VisitManagerTest
    {
        private Mock<IVisitRepository> mockRepo;

        [Fact]
        public void AddVisit_Valid()
        {
            Visitor visitor = new Visitor("Tobias","tobiaswille@hotmail.com");
            Company visitedCompany = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "9000",null), "0479564251", "allphi@gmail.com");
            Employee visitedEmployee = EmployeeFactory.MakeEmployee(1, "Tobias", "Wille", "tobiaswille@hotmail.com","dev");
            mockRepo = new Mock<IVisitRepository>();
            Visit visit = new Visit(visitor, visitedCompany, visitedEmployee, new DateTime(2022, 11, 15, 12, 30, 00), new DateTime(2022, 11, 15, 13, 00, 00));
            mockRepo.Setup(repo => repo.AddVisit(visit));
            mockRepo.Setup(repo => repo.GetVisit(1)).Returns(visit);
        }
    }
}
