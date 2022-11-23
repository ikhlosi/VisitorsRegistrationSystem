using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;

namespace TestManagers
{
    public class VisitManagerTest
    {
        private VisitManager vm;
        private Mock<IVisitRepository> mockRepo;
        private Visitor visitor;

        [Fact]
        public void AddVisit_Valid()
        {
            Visitor visitor = new Visitor("Tobias","tobiaswille@hotmail.com","bedrijfA");
            Company visitedCompany = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "9000",null), "0479564251", "allphi@gmail.com");

            Employee visitedEmployee = EmployeeFactory.MakeEmployee(1, "Tobias", "Wille", "tobiaswille@hotmail.com","dev");
            mockRepo = new Mock<IVisitRepository>();
            Visit visit = new Visit(visitor, visitedCompany, visitedEmployee, new DateTime(2022, 11, 15, 12, 30, 00));
            mockRepo.Setup(repo => repo.AddVisit(visit));
            mockRepo.Setup(repo => repo.GetVisit(1)).Returns(visit);
        }
        [Fact]
        public void RemoveVisit_Valid()
        {
            Visitor visitor = new Visitor("Tobias", "tobiaswille@hotmail.com","bedrijfA");
            Company visitedCompany = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "9000", null), "0479564251", "allphi@gmail.com");
            Employee visitedEmployee = EmployeeFactory.MakeEmployee(1, "Tobias", "Wille", "tobiaswille@hotmail.com", "dev");
            mockRepo = new Mock<IVisitRepository>();
            Visit visit = new Visit(visitor, visitedCompany, visitedEmployee, new DateTime(2022, 11, 15, 12, 30, 00));
            mockRepo.Setup(repo => repo.RemoveVisit(visit.Id));
            mockRepo.Setup(repo => repo.GetVisit(1)).Returns(visit);
        }
        [Fact]
        public void UpdateVisit_Valid()
        {
            Visitor visitor = new Visitor("Tobias", "tobiaswille@hotmail.com","bedrijfA");
            Company visitedCompany = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "9000", null), "0479564251", "allphi@gmail.com");
            Employee visitedEmployee = EmployeeFactory.MakeEmployee(1, "Tobias", "Wille", "tobiaswille@hotmail.com", "dev");
            mockRepo = new Mock<IVisitRepository>();
            Visit visit = new Visit(visitor, visitedCompany, visitedEmployee, new DateTime(2022, 11, 15, 12, 30, 00));
            mockRepo.Setup(repo => repo.UpdateVisit(visit));
            mockRepo.Setup(repo => repo.GetVisit(1)).Returns(visit);
        }
        [Fact]
        public void VisitExists_Valid()
        {
            Visitor visitor = new Visitor("Tobias", "tobiaswille@hotmail.com", "bedrijfA");
            Company visitedCompany = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "9000", null), "0479564251", "allphi@gmail.com");
            Employee visitedEmployee = EmployeeFactory.MakeEmployee(1, "Tobias", "Wille", "tobiaswille@hotmail.com", "dev");
            mockRepo = new Mock<IVisitRepository>();
            Visit visit = new Visit(visitor, visitedCompany, visitedEmployee, new DateTime(2022, 11, 15, 12, 30, 00));
            mockRepo.Setup(repo => repo.VisitExists(visit));
            mockRepo.Setup(repo => repo.GetVisit(1)).Returns(visit);
        }

        // AddVisitor
        [Fact]
        public void Test_AddVisitor_Invalid_VisitorIsNull()
        {
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(null));
            Assert.Equal("VisitorManager - Addvisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_AddVisitor_Invalid_VisitorExistsInDB()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(this.visitor));
            Assert.Equal("VisitorManager - Addvisitor - visitor has already been registered", ex.InnerException.Message);
        }

        [Fact]
        public void Test_AddVisitor_Invalid_Catch()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
            this.mockRepo.Setup(x => x.AddVisitor(this.visitor)).Throws(new VisitorManagerException());
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(this.visitor));
            Assert.Equal("VisitorManager - AddVisitor", ex.Message);
        }

        // DeleteVisitor
        [Fact]
        public void Test_DeleteVisitor_Invalid_VisitorIsNull()
        {
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(null));
            Assert.Equal("VisitorManager - DeleteVisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_DeleteVisitor_Invalid_VisitorDoesNotExistInDB()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(this.visitor));
            Assert.Equal("VisitorManager - DeleteVisitor - visitor is not registered", ex.InnerException.Message);
        }

        [Fact]
        public void Test_DeleteVisitor_Invalid_Catch()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
            this.mockRepo.Setup(x => x.RemoveVisitor(this.visitor.Id)).Throws(new VisitorManagerException());
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(this.visitor));
            Assert.Equal("VisitorManager - DeleteVisitor", ex.Message);
        }

        // UpdateVisitor
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorIsNull()
        {
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(null));
            Assert.Equal("VisitorManager - UpdateVisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorDoesNotExistInDB()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
            Assert.Equal("VisitorManager - UpdateVisitor - visitor is not registered", ex.InnerException.Message);
        }
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorIsUnchanged()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
            this.mockRepo.Setup(x => x.GetVisitor(this.visitor.Id).Equals(visitor)).Returns(true);
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
            Assert.Equal("VisitorManager - UpdateVisitor - updated visitor is unchanged", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateVisitor_Invalid_Catch()
        {
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
            this.mockRepo.Setup(x => x.UpdateVisitor(this.visitor)).Throws(new VisitorManagerException());
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
            Assert.Equal("VisitorManager - UpdateVisitor", ex.Message);
        }

        // GetVisitors
        [Fact]
        public void Test_GetVisitors_Invalid_Catch()
        {
            this.mockRepo.Setup(x => x.GetAllVisitors()).Throws(new VisitorManagerException());
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.GetVisitors());
            Assert.Equal("VisitorManager - GetVisitors", ex.Message);
        }

        // GetVisitor
        [Fact]
        public void Test_GetVisitor_Invalid_Catch()
        {
            this.mockRepo.Setup(x => x.GetVisitor(this.visitor.Id)).Throws(new VisitorManagerException());
            this.mockRepo.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.GetVisitor(visitor.Id));
            Assert.Equal("VisitorManager - GetVisitor", ex.Message);
        }
    }
}
