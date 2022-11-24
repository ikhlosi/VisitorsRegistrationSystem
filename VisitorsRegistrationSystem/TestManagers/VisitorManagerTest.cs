////using Moq;
////using VisitorsRegistrationSystemBL.Domain;
////using VisitorsRegistrationSystemBL.Exceptions;
////using VisitorsRegistrationSystemBL.Factories;
////using VisitorsRegistrationSystemBL.Interfaces;
////using VisitorsRegistrationSystemBL.Managers;
////using Xunit;
////namespace TestManagers
////{
////    public class VisitorManagerTest
////    {
////        private VisitManager vm;
////        private Mock<IVisitRepository> visitRepoMock;
////        private Company company;
////        private Visit visit;

//        public VisitorManagerTest()
//        {
//            this.visitRepoMock = new Mock<IVisitRepository>();
//            this.vm = new VisitManager(visitRepoMock.Object);
//            this.company = CompanyFactory.MakeCompany(1, "Allphi", "BE123456789", new Address("Elsegem", "Kouterlos", "60000", null), "0479564251", "allphi@gmail.com");
//            this.visitor = VisitorFactory.MakeVisitor(1, "Arno Vantieghem", "arnovantieghem@gmail.com", "bizz");
//        }
//        // AddVisitor
//        [Fact]
//        public void Test_AddVisitor_Invalid_VisitorIsNull()
//        {
//            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(null));
//            Assert.Equal("VisitorManager - Addvisitor - visitor is null", ex.Message);
//        }
//        [Fact]
//        public void Test_AddVisitor_Invalid_VisitorExistsInDB()
//        {
//            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
//            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(this.visitor));
//            Assert.Equal("VisitorManager - Addvisitor - visitor has already been registered", ex.InnerException.Message);
//        }

////        [Fact]
////        public void Test_AddVisitor_Invalid_Catch()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
////            this.visitorRepoMock.Setup(x => x.AddVisitor(this.visitor)).Throws(new VisitorManagerException());
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.AddVisitor(this.visitor));
////            Assert.Equal("VisitorManager - AddVisitor", ex.Message);
////        }

////        // DeleteVisitor
////        [Fact]
////        public void Test_DeleteVisitor_Invalid_VisitorIsNull()
////        {
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(null));
////            Assert.Equal("VisitorManager - DeleteVisitor - visitor is null", ex.Message);
////        }
////        [Fact]
////        public void Test_DeleteVisitor_Invalid_VisitorDoesNotExistInDB()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(this.visitor));
////            Assert.Equal("VisitorManager - DeleteVisitor - visitor is not registered", ex.InnerException.Message);
////        }

////        [Fact]
////        public void Test_DeleteVisitor_Invalid_Catch()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
////            this.visitorRepoMock.Setup(x => x.RemoveVisitor(this.visitor.Id)).Throws(new VisitorManagerException());
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.DeleteVisitor(this.visitor));
////            Assert.Equal("VisitorManager - DeleteVisitor", ex.Message);
////        }

////        // UpdateVisitor
////        [Fact]
////        public void Test_UpdateVisitor_Invalid_VisitorIsNull()
////        {
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(null));
////            Assert.Equal("VisitorManager - UpdateVisitor - visitor is null", ex.Message);
////        }
////        [Fact]
////        public void Test_UpdateVisitor_Invalid_VisitorDoesNotExistInDB()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
////            Assert.Equal("VisitorManager - UpdateVisitor - visitor is not registered", ex.InnerException.Message);
////        }
////        [Fact]
////        public void Test_UpdateVisitor_Invalid_VisitorIsUnchanged()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
////            this.visitorRepoMock.Setup(x => x.GetVisitor(this.visitor.Id).Equals(visitor)).Returns(true);
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
////            Assert.Equal("VisitorManager - UpdateVisitor - updated visitor is unchanged", ex.InnerException.Message);
////        }

////        [Fact]
////        public void Test_UpdateVisitor_Invalid_Catch()
////        {
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(false);
////            this.visitorRepoMock.Setup(x => x.UpdateVisitor(this.visitor)).Throws(new VisitorManagerException());
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.UpdateVisitor(this.visitor));
////            Assert.Equal("VisitorManager - UpdateVisitor", ex.Message);
////        }

////        // GetVisitors
////        [Fact]
////        public void Test_GetVisitors_Invalid_Catch()
////        {
////            this.visitorRepoMock.Setup(x => x.GetAllVisitors()).Throws(new VisitorManagerException());
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.GetVisitors());
////            Assert.Equal("VisitorManager - GetVisitors", ex.Message);
////        }

////        // GetVisitor
////        [Fact]
////        public void Test_GetVisitor_Invalid_Catch()
////        {
////            this.visitorRepoMock.Setup(x => x.GetVisitor(this.visitor.Id)).Throws(new VisitorManagerException());
////            this.visitorRepoMock.Setup(x => x.VisitorExists(this.visitor.Id)).Returns(true);
////            var ex = Assert.Throws<VisitorManagerException>(() => this.vm.GetVisitor(visitor.Id));
////            Assert.Equal("VisitorManager - GetVisitor", ex.Message);
////        }


////    }
////}