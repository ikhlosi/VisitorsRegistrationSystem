using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;

namespace TestManagers
{
    public class EmployeeManagerTest
    {
        private CompanyManager _cm;
        private Mock<ICompanyRepository> _companyRepoMock;
        private Employee _validEmployee;

        public EmployeeManagerTest() {
            this._validEmployee = EmployeeFactory.MakeEmployee(1, "John", "Doe", null, "Senior Software Developer");
            this._companyRepoMock = new Mock<ICompanyRepository>();
            this._cm = new CompanyManager(_companyRepoMock.Object);
        }

        [Fact]
        public void Test_AddEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._cm.AddEmployee(null));
            Assert.Equal("EmployeeManager - Addemployee - employee is null", ex.Message);
        }

        // test removed because checking if employee exists will be done in UI
        //[Fact]
        //public void Test_AddEmployee_invalid_EmployeeExistsInDB() {
        //    this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
        //    var ex = Assert.Throws<EmployeeException>(() => this._em.AddEmployee(this._validEmployee));
        //    Assert.Equal("EmployeeManager - AddEmployee - employee already exists in DB.", ex.InnerException.Message);
        //}



        [Fact]       
        public void Test_AddEmployee_invalid_catch() {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            this._companyRepoMock.Setup(x => x.WriteEmployeeInDB(this._validEmployee)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._cm.AddEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - AddEmployee", ex.Message);
        }


        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._cm.RemoveEmployee(null));
            Assert.Equal("EmployeeManager - RemoveEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeNotInDB() {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<EmployeeException>(() => this._cm.RemoveEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - RemoveEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_RemoveEmployee_invalid_catch() {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.RemoveEmployeeFromDB(this._validEmployee.ID)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._cm.RemoveEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - RemoveEmployee", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._cm.UpdateEmployee(null));
            Assert.Equal("EmployeeManager - UpdateEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeNotInDB() {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<EmployeeException>(() => this._cm.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_IsSame() {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(this._validEmployee);
            var ex = Assert.Throws<EmployeeException>(() => this._cm.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee - fields are the same, nothing to update.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_UpdateEmployee_invalid_catch() {
            Employee differentValidEmployee = EmployeeFactory.MakeEmployee(null, "James", "Jackson", null, "Project Manager");
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(differentValidEmployee);
            this._companyRepoMock.Setup(x => x.UpdateEmployeeInDB(this._validEmployee)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._cm.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee", ex.Message);
        }

        [Fact]       
        public void Test_GetEmployees_invalid_catch() {
            this._companyRepoMock.Setup(x => x.GetEmployeesFromDB()).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._cm.GetEmployees());
            Assert.Equal("EmployeeManager - GetEmployees", ex.Message);
        }

    }
}
