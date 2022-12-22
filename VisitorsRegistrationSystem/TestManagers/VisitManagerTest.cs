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
        // TODO endtime gaat voor sommige tests nog ingesteld moeten worden aangezien we dat NIET MEER definieren in de constructor
        private Mock<IVisitRepository> _mockRepo;
        private Visitor _validVisitor;
        private VisitManager _vm;
        //private int x;
        private Company _visitedCompany;
        private Employee _employee;
        private Visitor _visitor;
        public VisitManagerTest()
        {
            //this.x = 5;
            this._mockRepo = new Mock<IVisitRepository>();
            this._validVisitor = VisitorFactory.MakeVisitor(null, "tony", "tonytonychopper@hotmail.com", "CompanyVisitor");
            this._vm = new VisitManager(this._mockRepo.Object);
            this._visitedCompany = CompanyFactory.MakeCompany(null, "companyA", "xxxxxx", new Address("Gent", "9000", "Sleepstraat", "2", null), "0471970495", "companyA@hotmail.com");
            this._employee = EmployeeFactory.MakeEmployee(null, "Luffy", "Monkey D", "MonkeyDLuffy@hotmail.com", "CEO", null);
            this._visitor = VisitorFactory.MakeVisitor(null, "jos", "jos@hotmail.com", "CompanyV");
        }

        [Fact]
        public void AddVisit_Invalid_Visit_is_null()
        {
            // voorbereiden
            Visit visit = null;
            // testen
            var ex = Assert.Throws<VisitException>( () => this._vm.AddVisit(visit) );
            Assert.Equal("VisitManager(AddVisit) - visit is null", ex.Message);
        }

        [Fact]
        public void AddVisit_Invalid_Visit_exists()
        {
            // voorbereiden
            Visit v = VisitFactory.MakeVisit(null, this._validVisitor, this._visitedCompany, this._employee);
            this._mockRepo.Setup(repoInterface => repoInterface.VisitExists(v)).Returns(true);

            // testen
            var ex = Assert.Throws<VisitException>( () => this._vm.AddVisit(v) );
            Assert.Equal("VisitManager - AddVisit - Visit does exist", ex.Message);

        }
        [Fact]
        public void DeleteVisit_Invalid_Visit_is_null()
        {
            //voorbereiden
            Visit visit = null;
            //testen
            var ex = Assert.Throws<VisitException>(() => this._vm.DeleteVisit(visit) );
            Assert.Equal("VisitManager(Deletevisit) - visit is null", ex.Message);
        }
        [Fact]
        public void DeleteVisist_Invalid_Visit_NotExist()
        {
            //voorbereiden
            Visit visit = VisitFactory.MakeVisit(null,_validVisitor,_visitedCompany,_employee);
            this._mockRepo.Setup(repoInterface => repoInterface.VisitExists(visit)).Returns(false);
            //testen
            var ex=Assert.Throws<VisitException>(() => this._vm.DeleteVisit(visit));
            Assert.Equal("VisitManager - Deletevisit - visit does not exist", ex.Message);
        }
        [Fact]
        public void UpdateVisit_Invalid_Visit_is_null()
        {
            Visit visit = null;
            //testen
            var ex = Assert.Throws<VisitException>(() => this._vm.UpdateVisit(visit));
            Assert.Equal("VisitManager(Updatevisit) - visit is null", ex.Message);
        }
        [Fact]
        public void UpdateVisit_Invalid_Visit_NotExist()
        {
            Visit visit = VisitFactory.MakeVisit(null,_validVisitor,_visitedCompany,_employee);
            this._mockRepo.Setup(repoInterface => repoInterface.VisitExists(visit)).Returns(false);
            //testen
            var ex = Assert.Throws<VisitException>(() => this._vm.UpdateVisit(visit));
            Assert.Equal("VisitManager(Updatevisit) - visit does not exist", ex.InnerException.Message);
        }
        [Fact]
        public void UpdateVisit_Invalid_Visit_is_unchanged()
        {
            // id 1 moet meegegeven worden want je kan een visit niet updaten als je de id ervan niet weet
            Visit visit = VisitFactory.MakeVisit(1, _validVisitor, _visitedCompany, _employee);
            this._mockRepo.Setup(repoInterface => repoInterface.VisitExists(visit)).Returns(true);
            this._mockRepo.Setup(repoInterface => repoInterface.GetVisit(visit.Id)).Returns(visit);
            //testen
            var ex = Assert.Throws<VisitException>(() => this._vm.UpdateVisit(visit));
            Assert.Equal("VisitManager(Updatevisit) - visit is unchanged", ex.InnerException.Message);
        }

        //--------- Visistor part -----------
        [Fact]
        public void Test_AddVisitor_Invalid_VisitorIsNull()
        {
            Visitor visitor = null;
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.AddVisitor(null));
            Assert.Equal("VisitManager - Addvisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_AddVisitor_Invalid_VisitorExistsInDB()
        {
            this._mockRepo.Setup(repoInterface => repoInterface.VisitorExists(this._visitor.Id)).Returns(true);
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.AddVisitor(this._visitor));
            Assert.Equal("VisitManager - Addvisitor - visitor has already been registered", ex.InnerException.Message);
        }

        [Fact]
        public void Test_AddVisitor_Invalid_Catch()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(false);
            this._mockRepo.Setup(x => x.AddVisitor(this._visitor)).Throws(new VisitManagerException());
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.AddVisitor(this._visitor));
            Assert.Equal("VisitManager - AddVisitor", ex.Message);
        }

        // DeleteVisitor
        [Fact]
        public void Test_DeleteVisitor_Invalid_VisitorIsNull()
        {
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.DeleteVisitor(null));
            Assert.Equal("VisitManager - DeleteVisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_DeleteVisitor_Invalid_VisitorDoesNotExistInDB()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(false);
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.DeleteVisitor(this._visitor));
            Assert.Equal("VisitManager - DeleteVisitor - visitor is not registered", ex.InnerException.Message);
        }

        [Fact]
        public void Test_DeleteVisitor_Invalid_Catch()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(true);
            this._mockRepo.Setup(x => x.RemoveVisitor(this._visitor.Id)).Throws(new VisitManagerException());
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.DeleteVisitor(this._visitor));
            Assert.Equal("VisitManager - DeleteVisitor", ex.Message);
        }
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorIsNull()
        {
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.UpdateVisitor(null));
            Assert.Equal("VisitManager - UpdateVisitor - visitor is null", ex.Message);
        }
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorDoesNotExistInDB()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(false);
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.UpdateVisitor(this._visitor));
            Assert.Equal("VisitManager - UpdateVisitor - visitor is not registered", ex.InnerException.Message);
        }
        [Fact]
        public void Test_UpdateVisitor_Invalid_VisitorIsUnchanged()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(true);
            this._mockRepo.Setup(x => x.GetVisitor(this._visitor.Id).Equals(_visitor)).Returns(true);
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.UpdateVisitor(this._visitor));
            Assert.Equal("VisitManager - UpdateVisitor - updated visitor is unchanged", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateVisitor_Invalid_Catch()
        {
            this._mockRepo.Setup(x => x.VisitorExists(this._visitor.Id)).Returns(false);
            this._mockRepo.Setup(x => x.UpdateVisitor(this._visitor)).Throws(new VisitManagerException());
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.UpdateVisitor(this._visitor));
            Assert.Equal("VisitManager - UpdateVisitor", ex.Message);
        }

        // GetVisitors
        [Fact]
        public void Test_GetVisitors_Invalid_Catch()
        {
            this._mockRepo.Setup(x => x.GetAllVisitors()).Throws(new VisitManagerException());
            var ex = Assert.Throws<VisitManagerException>(() => this._vm.GetVisitors());
            Assert.Equal("VisitManager - GetVisitors", ex.Message);
        }
    }
}
