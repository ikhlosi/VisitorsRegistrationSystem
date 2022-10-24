using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using Xunit;
namespace TestManagers
{
    public class VisitorManagerTest
    {
        private Mock<IVisitorRepository> mockRepo;

        [Fact]
        public void AddVisitor_Valid()
        {
            mockRepo = new Mock<IVisitorRepository>();
            Company company = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "600000"), "0479564251", "allphi@gmail.com");
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Arno Vantieghem", "arnovantieghem@gmail.com", company);
            mockRepo.Setup(repo => repo.AddVisitor(visitor));
            mockRepo.Setup(repo => repo.GetVisitor(1)).Returns(visitor);
        }
    }
}