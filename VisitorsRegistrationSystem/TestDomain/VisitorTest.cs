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
        private Visitor _validVisitor;
        public VisitorTest() {
            _validVisitor = new Visitor("John", "john@outlook.com");
        }

        [Theory]
        [InlineData("John", "john@outlook.com")]
        [InlineData("geert", "geert@outlook.com")]
        public void Test_ctor_valid(string name, string email) {
            #region Arrange
            Company company = CompanyFactory.MakeCompany(null, "CompanyA", "XXXXXXXX", null, null, "companyA@outlook.com");
            #endregion
            #region Act
            Visitor v = new Visitor(name, email);
            //Visitor v2 = new Visitor(name, email, company);
            #endregion
            #region Assert
            Assert.Equal(name, v.Name);
            Assert.Equal(email, v.Email);
            //Assert.Equal(name, v2.Name);
            //Assert.Equal(email, v2.Email);
            //Assert.Equal(company, v2.VisitorCompany);
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
            #region Act - Assert
            var ex = Assert.Throws<VisitorException>(() => v = new Visitor(name, email));
            Assert.Equal("Visitor - Name is null or whitespace", ex.Message);
            //Assert.Null(v);
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
            #region Act - Assert
            var ex = Assert.Throws<VisitorException>(() => v = new Visitor(name, email));
            if (string.IsNullOrWhiteSpace(email)) {
                Assert.Equal("Visitor - Email is null or whitespace", ex.Message);
            } else {
                Assert.Equal("Visitor - Email format invalid", ex.Message);
                // todo: invalid email check: check whether EmailCheckerException is thrown
            }
            #endregion
        }
        //[Fact]
        //public void Test_ctor_invalid_visitorCompany() {
        //    #region Arrange
        //    Company c = null;
        //    Visitor v;
        //    #endregion
        //    #region Assert
        //    //var ex = Assert.Throws<VisitorException>( () => v = new Visitor("John", "john@outlook.com", c) );
        //    var ex = Assert.Throws<VisitorException>( () => v = new Visitor("John", "john@outlook.com") );
        //    Assert.Equal("Visitor - Visitorcompany is null", ex.Message);
        //    #endregion
        //}

        [Fact]
        public void Test_SetVisitorCompany_valid() {
            #region Arrange
            // Visitor v = new Visitor("John", "john@outlook.com");
            Company c = new Company("CompanyA", "XXXXXXXXX", "CompanyA@outlook.com");
            #endregion
            #region Act
            _validVisitor.setVisitorCompany(c);
            #endregion
            #region Assert
            Assert.NotNull(_validVisitor.VisitorCompany);
            Assert.Equal(c, _validVisitor.VisitorCompany);
            #endregion
        }

        [Theory]
        [InlineData(null)]
        public void Test_SetVisitorCompany_invalid(Company c) {
            #region Arrange
            // Visitor v = new Visitor("John", "john@outlook.com");
            #endregion
            #region Act - Assert
            var ex = Assert.Throws<VisitorException>(() => _validVisitor.setVisitorCompany(c));
            Assert.Equal("Visitor - Visitorcompany is null", ex.Message);
            Assert.Null(_validVisitor.VisitorCompany);
            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void Test_SetId_valid(int id) {
            _validVisitor.setId(id);
            Assert.Equal(id, _validVisitor.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_SetId_invalid(int id) {
            var ex = Assert.Throws<VisitorException>(() => _validVisitor.setId(id));
            Assert.Equal("Visitor - invalid Id", ex.Message);
            // Assert.Null(_validVisitor.Id);
        }

        [Fact]
        public void Test_Equals_valid() {
            // Arrange
            _validVisitor.setId(1);
            // Act
            Visitor v = new Visitor(_validVisitor.Name, _validVisitor.Email);
            v.setId(_validVisitor.Id);
            // Assert
            Assert.True(_validVisitor.Equals(v));
        }
        // todo: unit test Visitor Equals()
    }
}
