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
        private VisitorManager vm;
        private Mock<IVisitorRepository> visitorRepoMock;
        private Company company;
        private Visitor visitor;

        public VisitorManagerTest()
        {
            this.visitorRepoMock = new Mock<IVisitorRepository>();
            this.vm = new VisitorManager(visitorRepoMock.Object);
            this.company = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "60000", null), "0479564251", "allphi@gmail.com");
            this.visitor = VisitorFactory.MakeVisitor(1, "Arno Vantieghem", "arnovantieghem@gmail.com", "bizz");
        }


    }
}