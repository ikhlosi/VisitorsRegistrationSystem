using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;

namespace TestDomain {
    public class VisitorTest {
        [Theory]
        [InlineData("John", "john@outlook.com")]
        [InlineData("geert", "geert@outlook.com")]
        public void Test_ctor_valid(string name, string email) {
            #region Arrange
            Company company = CompanyFactory.MakeCompany(null, "CompanyA", "XXXXXXXX", null, null, "companyA@outlook.com");
            #endregion
            #region Act
            Visitor v = new Visitor(name, email);
            Visitor v2 = new Visitor(name, email, company);
            #endregion
            #region Assert
            Assert.Equal(name, v.Name);
            Assert.Equal(email, v.Email);
            Assert.Equal(name, v2.Name);
            Assert.Equal(email, v2.Email);
            Assert.Equal(company, v2.VisitorCompany);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" \n ")]
        [InlineData("      ")]
        public void Test_ctor_invalid_name(string name) {
            #region Arrange
            Visitor v;
            string email = "john@outlook.com";
            #endregion
            #region Assert
            var ex = Assert.Throws<VisitorException>(() => v = new Visitor(name, email));
            Assert.Equal("Visitor - Name is null or whitespace", ex.Message);
            #endregion
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" \n ")]
        [InlineData("      ")]
        [InlineData("john.com")]
        [InlineData("john")]
        public void Test_ctor_invalid_email(string email) {
            #region Arrange
            Visitor v;
            string name = "John";
            #endregion
            #region Assert
            
            var ex = Assert.Throws<VisitorException>(() => v = new Visitor(name, email));
            if (string.IsNullOrWhiteSpace(email)) {
                Assert.Equal("Visitor - Email is null or whitespace", ex.Message);
            } else {
                Assert.Equal("Visitor - Email format invalid", ex.Message);
                // todo: invalid email check: check whether EmailCheckerException is thrown
            }
            #endregion
        }
        [Fact]
        public void Test_ctor_invalid_visitorCompany() {
            #region Arrange
            Company c = null;
            Visitor v;
            #endregion
            #region Assert
            var ex = Assert.Throws<VisitorException>( () => v = new Visitor("John", "john@outlook.com", c) );
            Assert.Equal("Visitor - Visitorcompany is null", ex.Message);
            #endregion
        }

        // todo: unit test Visitor Id
        // todo: unit test Visitor Equals()
    }
}
