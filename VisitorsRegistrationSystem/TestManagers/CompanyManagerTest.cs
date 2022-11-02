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
        private Company _validCompany;

        public CompanyManagerTest() {
            this._validCompany = CompanyFactory.MakeCompany(null, "CompanyA", "XXXXXXXXX", null, null, "companya@outlook.com");
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
        public void Test_AddCompany_invalid_CompanyNotInDB() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(false);
            var ex = Assert.Throws<CompanyException>(() => this._cm.RemoveCompany(this._validCompany));
            Assert.Equal("CompanyManager - RemoveCompany - company does not exist in DB.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_RemoveCompany_invalid_catch() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.RemoveCompanyFromDB(this._validCompany)).Throws(new CompanyException());
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
        public void Test_UpdateCompany_invalid_IsSame() {
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetCompany(this._validCompany.ID)).Returns(this._validCompany);
            var ex = Assert.Throws<CompanyException>(() => this._cm.UpdateCompany(this._validCompany));
            Assert.Equal("CompanyManager - UpdateCompany - fields are the same, nothing to update.", ex.InnerException.Message);
        }

        [Fact]       
        public void Test_UpdateCompany_invalid_catch() {
            Company differentValidCompany = CompanyFactory.MakeCompany(null, "CompanyB", "YYYYYYYYY", null, null, "companyb@outlook.com");
            this._companyRepoMock.Setup(x => x.CompanyExistsInDB(this._validCompany.ID)).Returns(true);
            this._companyRepoMock.Setup(x => x.GetCompany(this._validCompany.ID)).Returns(differentValidCompany);
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
    }
}
