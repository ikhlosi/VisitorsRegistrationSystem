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
        private EmployeeManager _em;
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private Employee _validEmployee;

        public EmployeeManagerTest() {
            this._validEmployee = EmployeeFactory.MakeEmployee(null, "John", "Doe", null, "Senior Software Developer");
            this._employeeRepoMock = new Mock<IEmployeeRepository>();
            this._em = new EmployeeManager(_employeeRepoMock.Object);
        }

        [Fact]
        public void Test_AddEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._em.AddEmployee(null));
            Assert.Equal("EmployeeManager - Addemployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_AddEmployee_invalid_EmployeeExistsInDB() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee)).Returns(true);
            var ex = Assert.Throws<EmployeeException>(() => this._em.AddEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - AddEmployee - employee already exists in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_AddEmployee_invalid_catch() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee)).Returns(false);
            this._employeeRepoMock.Setup(x => x.WriteEmployeeInDB(this._validEmployee)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._em.AddEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - AddEmployee", ex.Message);
        }


        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._em.RemoveEmployee(null));
            Assert.Equal("EmployeeManager - RemoveEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeNotInDB() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<EmployeeException>(() => this._em.RemoveEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - RemoveEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_RemoveEmployee_invalid_catch() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._employeeRepoMock.Setup(x => x.RemoveEmployeeFromDB(this._validEmployee.ID)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._em.RemoveEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - RemoveEmployee", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeIsNull() {
            var ex = Assert.Throws<EmployeeException>(() => this._em.UpdateEmployee(null));
            Assert.Equal("EmployeeManager - UpdateEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeNotInDB() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<EmployeeException>(() => this._em.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_IsSame() {
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._employeeRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(this._validEmployee);
            var ex = Assert.Throws<EmployeeException>(() => this._em.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee - fields are the same, nothing to update.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_UpdateEmployee_invalid_catch() {
            Employee differentValidEmployee = EmployeeFactory.MakeEmployee(null, "James", "Jackson", null, "Project Manager");
            this._employeeRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._employeeRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(differentValidEmployee);
            this._employeeRepoMock.Setup(x => x.UpdateEmployeeInDB(this._validEmployee)).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._em.UpdateEmployee(this._validEmployee));
            Assert.Equal("EmployeeManager - UpdateEmployee", ex.Message);
        }

        [Fact]       
        public void Test_GetEmployees_invalid_catch() {
            this._employeeRepoMock.Setup(x => x.GetEmployeesFromDB()).Throws(new EmployeeException());
            var ex = Assert.Throws<EmployeeException>(() => this._em.GetEmployees());
            Assert.Equal("EmployeeManager - GetEmployees", ex.Message);
        }

    }
}
