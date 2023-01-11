using Moq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;

namespace TestManagers
{
    public class CompanyManagerTest
    {
        private CompanyManager _cm;
        private Mock<ICompanyRepository> _companyRepoMock;
        private Employee _validEmployee;
        private Company _validCompany;

        public CompanyManagerTest() {
            this._validEmployee = EmployeeFactory.MakeEmployee(1, "John", "Doe", null, "Senior Software Developer",null);
            this._validCompany = CompanyFactory.MakeCompany(1, "CompanyA", "BE0400378485", new Address("Gent","9000","kerkstraat","57","B"), "0471970495", "companya@outlook.com");
            this._companyRepoMock = new Mock<ICompanyRepository>();
            this._cm = new CompanyManager(_companyRepoMock.Object);
        }

        [Fact]
        public void Test_AddCompany_invalid_CompanyIsNull() {
            var ex = Assert.Throws<CompanyException>(() => this._cm.AddCompany(null));
            Assert.Equal("CompanyManager - AddCompany - company is null.", ex.Message);
        }

        [Fact]
        public void Test_AddCompany_invalid_CompanyExistsInDB() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany)).Returns(true);
            var ex = Assert.Throws<CompanyException>(() => this._cm.AddCompany(this._validCompany));
            Assert.Equal("CompanyManager - AddCompany - company already exists in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_AddCompany_invalid_catch() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany)).Returns(false);
            this._companyRepoMock.Setup(x => x.WriteCompanyInDB(this._validCompany)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.AddCompany(this._validCompany));
            Assert.Equal("CompanyManager - AddCompany", ex.Message);
            // todo: ask if need another test when _repo.CompanyExistsInDB() throws exception
        }


        [Fact]
        public void Test_RemoveCompany_invalid_CompanyIsNull() {
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveCompany(null));
            Assert.Equal("CompanyManager - RemoveCompany - company is null.", ex.Message);
        }

        [Fact]
        public void Test_RemoveCompany_invalid_CompanyNotInDB() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(false);
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveCompany(this._validCompany));
            Assert.Equal("CompanyManager - RemoveCompany - company does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_RemoveCompany_invalid_catch() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.RemoveCompanyFromDB(this._validCompany.ID)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveCompany(this._validCompany));
            Assert.Equal("CompanyManager - RemoveCompany", ex.Message);
        }

        [Fact]
        public void Test_UpdateCompany_invalid_CompanyIsNull() {
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateCompany(null));
            Assert.Equal("CompanyManager - UpdateCompany - company is null.", ex.Message);
        }

        [Fact]
        public void Test_UpdateCompany_invalid_CompanyNotInDB() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(false);
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateCompany(this._validCompany));
            Assert.Equal("CompanyManager - UpdateCompany - company does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateCompany_invalid_IsSame()
        {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetCompanyByIdFromDB(this._validCompany.ID)).Returns(this._validCompany);
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateCompany(this._validCompany));
            Assert.Equal("CompanyManager - UpdateCompany - fields are the same, nothing to update.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateCompany_invalid_catch()
        {
            Company differentValidCompany = CompanyFactory.MakeCompany(null, "CompanyB", "YYYYYYYY", null, null, "companyb@outlook.com");
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetCompanyByIdFromDB(this._validCompany.ID)).Returns(differentValidCompany);
            this._companyRepoMock.Setup(x => x.UpdateCompanyInDB(this._validCompany)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateCompany(this._validCompany));
            Assert.Equal("CompanyManager - UpdateCompany", ex.Message); 
        }

        [Fact]       
        public void Test_GetCompanies_invalid_catch() {
            this._companyRepoMock.Setup(x => x.GetCompaniesFromDB()).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.GetCompanies());
            Assert.Equal("CompanyManager - GetCompanies", ex.Message);
        }
        //--------------------Employee part--------------------------
        [Fact]
        public void Test_AddEmployee_invalid_EmployeeIsNull()
        {
            var ex = Assert.Throws<CompanyException>(() => this._cm.AddEmployee(null, this._validCompany));
            Assert.Equal("CompanyManager - Addemployee - employee is null", ex.Message);
        }

        // test removed because checking if employee exists will be done in UI
        //[Fact]
        //public void Test_AddEmployee_invalid_EmployeeExistsInDB() {
        //    this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
        //    var ex = Assert.Throws<EmployeeException>(() => this._em.AddEmployee(this._validEmployee));
        //    Assert.Equal("EmployeeManager - AddEmployee - employee already exists in DB.", ex.InnerException.Message);
        //}



        [Fact]
        public void Test_AddEmployee_invalid_catch()
        {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            this._companyRepoMock.Setup(x => x.WriteEmployeeInDB(this._validEmployee, this._validCompany)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.AddEmployee(this._validEmployee, this._validCompany));
            Assert.Equal("CompanyManager - AddEmployee", ex.Message);
        }


        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeIsNull()
        {
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveEmployee(null));
            Assert.Equal("CompanyManager - RemoveEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_RemoveEmployee_invalid_EmployeeNotInDB()
        {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveEmployee(this._validEmployee));
            Assert.Equal("CompanyManager - RemoveEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_RemoveEmployee_invalid_catch()
        {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.RemoveEmployeeFromDB(this._validEmployee.ID)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveEmployee(this._validEmployee));
            Assert.Equal("CompanyManager - RemoveEmployee", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeIsNull()
        {
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateEmployee(null, this._validCompany));
            Assert.Equal("CompanyManager - UpdateEmployee - employee is null", ex.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_EmployeeNotInDB()
        {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(false);
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateEmployee(this._validEmployee, this._validCompany));
            Assert.Equal("CompanyManager - UpdateEmployee - employee does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_IsSame()
        {
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(this._validEmployee);
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateEmployee(this._validEmployee, this._validCompany));
            Assert.Equal("CompanyManager - UpdateEmployee - fields are the same, nothing to update.", ex.InnerException.Message);
        }

        [Fact]
        public void Test_UpdateEmployee_invalid_catch()
        {
            Employee differentValidEmployee = EmployeeFactory.MakeEmployee(null, "James", "Jackson", null, "Project Manager",null);
            this._companyRepoMock.Setup(x => x.EmployeeExistsInDB(this._validEmployee.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetEmployee(this._validEmployee.ID)).Returns(differentValidEmployee);
            this._companyRepoMock.Setup(x => x.UpdateEmployeeInDB(this._validEmployee, this._validCompany)).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateEmployee(this._validEmployee, this._validCompany));
            Assert.Equal("CompanyManager - UpdateEmployee", ex.Message);
        }

        [Fact]
        public void Test_GetEmployees_invalid_catch()
        {
            this._companyRepoMock.Setup(x => x.GetEmployeesFromDB()).Throws(new CompanyException());
            var ex = Assert.Throws<CompanyException>(() => this._cm.GetEmployees());
            Assert.Equal("CompanyManager - GetEmployees", ex.Message);
        }
    }
}
