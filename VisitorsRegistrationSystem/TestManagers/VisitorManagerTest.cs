using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;
using Xunit;
namespace TestManagers
{
    public class VisitorManagerTest
    {
        private Mock<IVisitorRepository> mockRepo;

        [Fact]
        public void AddVisitor_Valid()
        {
            Company company = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "60000", null), "0479564251", "allphi@gmail.com");
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Arno Vantieghem", "arnovantieghem@gmail.com", company);

            mockRepo = new Mock<IVisitorRepository>();
            mockRepo.Setup(repo => repo.AddVisitor(visitor));
            VisitorManager vm = new VisitorManager(mockRepo.Object);

            Assert.Equal(visitor, vm.GetVisitor(visitor.Id));
        }
    }
}