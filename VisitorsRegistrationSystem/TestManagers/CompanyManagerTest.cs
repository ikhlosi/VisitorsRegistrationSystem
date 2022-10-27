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
        [Fact]
        public void Test_AddCompany_invalid_CompanyExistsInDB() {
            Company c = CompanyFactory.MakeCompany(null, "CompanyA", "XXXXXXXXX", null, null, "companya@outlook.com");
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            companyRepoMock.Setup(x => x.CompanyExistsInDB(c)).Returns(true);
            CompanyManager CM = new CompanyManager(companyRepoMock.Object);
            var ex = Assert.Throws<CompanyException>(() => CM.AddCompany(c));
            Assert.Equal("CompanyManager - AddCompany - company already exists in DB.", ex.InnerException.Message);
        }
    }
}
