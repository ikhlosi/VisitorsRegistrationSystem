using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;
using Xunit;
using Xunit.Sdk;


namespace TestDomain
{
    public class CompanyTest
    {
        #region Company Domain Setters

        [Fact]
        public void SetID_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act
            companyA.SetID(2);
            #endregion
            #region Assert
            Assert.Equal(2, companyA.ID);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetID_CompanyException(int id)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetID(id));
            #endregion
        }

        [Fact]
        public void SetName_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act
            companyA.SetName("Company B");
            #endregion
            #region Assert
            Assert.Equal("Company B", companyA.Name);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetName_CompanyException(string name)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetName(name));
            #endregion
        }

        [Fact]
        public void SetVATNo_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act
            companyA.SetVATNo("BE1234567891");
            #endregion
            #region Assert
            Assert.Equal("BE1234567891", companyA.VATNumber);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetVATNo_CompanyException(string vATNumber)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetVATNo(vATNumber));
            #endregion
        }

        [Fact]
        public void SetAddress_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            Address expectedResult = new Address("Gent", "Straatlaan", "11");
            #endregion
            #region Act
            companyA.Address.SetHouseNo("11");
            #endregion
            #region Assert
            Assert.Equal(expectedResult, companyA.Address);
            #endregion
        }

        [Theory]
        [InlineData(null)]
        public void SetAddress_CompanyException(Address address)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetAddress(address));
            #endregion
        }

        [Fact]
        public void SetTelNo_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act
            companyA.SetTelNo("+32482455643");
            #endregion
            #region Assert
            Assert.Equal("+32482455643", companyA.TelephoneNumber);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetTelNo_CompanyException(string telNo)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetTelNo(telNo));
            #endregion
        }

        [Fact]
        public void SetEmail_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act
            companyA.SetEmail("company2@company.be");
            #endregion
            #region Assert
            Assert.Equal("company2@company.be", companyA.Email);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetEmail_CompanyException(string email)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.SetEmail(email));
            #endregion
        }

        #endregion

        #region Employee Manager

        [Fact]
        public void AddEmployee_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            Employee expectedResult = EmployeeFactory.MakeEmployee(1, "Bart", "Jan", "bart@company.be", "admin");
            #endregion
            #region Act
            companyA.AddEmployee(expectedResult);
            #endregion
            #region Assert
            Assert.Equal(expectedResult, companyA._employees[0]);
            #endregion
        }

        [Theory]
        [InlineData(null)]
        public void AddEmployee_CompanyException(Employee employee)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.AddEmployee(employee));
            #endregion
        }

        [Fact]
        public void RemoveEmployee_Success()
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            Employee expectedResult = EmployeeFactory.MakeEmployee(1, "Bart", "Jan", "bart@company.be", "admin");
            companyA.AddEmployee(expectedResult);
            #endregion
            #region Act
            companyA.RemoveEmployee(expectedResult);
            #endregion
            #region Assert
            Assert.Empty(companyA._employees);
            #endregion
        }

        [Theory]
        [InlineData(null)]
        public void RemoveEmployee_CompanyException(Employee employee)
        {
            #region Arrange
            Company companyA = CompanyFactory.MakeCompany(1, "Company A", "BE1234567890", new Address("Gent", "Straatlaan", "10"), "+32482455642", "company@company.be");
            #endregion
            #region Act & Assert
            Assert.Throws<CompanyException>(() => companyA.RemoveEmployee(employee));
            #endregion
        }

        #endregion


    }
}
