using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemBL.Managers;
using Xunit;
using Xunit.Sdk;

namespace TestDomain
{
    public class VisitTest
    {
        [Fact]
        public void Visit_SetId_Valid()
        {
            Visit visit = VisitFactory.MakeVisit(2, new Visitor("Name", "Name@hotmail.com", "CompanyTest"), new Company("Name", "XXXXXX", "mail@hotmail.com"), new Employee("Name", "LastName", "Function"));
            Assert.Equal(2, visit.Id);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Visit_SetId_Invalid(int id)
        {
            var ex = Assert.Throws<VisitException>(() => VisitFactory.MakeVisit(id, new Visitor("Name", "Name@hotmail.com", "CompanyTest"), new Company("Name", "XXXXXX", "mail@hotmail.com"), new Employee("Name", "LastName", "Function")));
            Assert.Equal("Visit - SetId - id smaller than 1",ex.InnerException.Message);
        }

        private Employee _employee;
        private Company _visitedCompany;
        private Visitor _visitor;
        public VisitTest()
        {
            this._employee = EmployeeFactory.MakeEmployee(1, "Luffy", "Monkey D", "MonkeyDLuffy@hotmail.com", "CEO");
            this._visitedCompany = CompanyFactory.MakeCompany(1, "companyA", "xxxxxx", new Address("Gent", "Sleepstraat", "2", null), "0471970495", "companyA@hotmail.com");
            this._visitor = VisitorFactory.MakeVisitor(1, "Ace", "Ace@hotmail.com", "CompanyV");
        }
        // TODO setten van endtime omdat we dit niet meer doen in constructor
        [Fact]
        public void Test_ctor_valid()
        {
            //Arrange
            Company company = _visitedCompany;
            Visitor visitor = _visitor;
            Employee employee = _employee;
            DateTime startTime = DateTime.Now.AddHours(1);

            //Act 
            Visit visit = VisitFactory.MakeVisit(1, _visitor, _visitedCompany, _employee);

            //Assert
            Assert.Equal(_visitor, visit.Visitor);
            Assert.Equal(_visitedCompany, visit.VisitedCompany);
            Assert.Equal(_employee, visit.VisitedEmployee);
            Assert.Equal(startTime, visit.StartTime);
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_VisitorSet(Visitor visitor)
        {
            //Arrange
            Visit v;
            Company company = _visitedCompany;
            Employee employee = _employee;
            DateTime startTime = DateTime.Now.AddHours(1);

            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(1, visitor, company, employee)); ;
            Assert.Equal("Visit - SetVisitor - Visitor is null", ex.InnerException.Message);
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_CompanySet(Company company)
        {
            //Arrange 
            Visit v;
            Visitor visitor = _visitor;
            Employee employee = _employee;
            DateTime startTime = DateTime.Now.AddHours(1);

            //Act 
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(1, visitor, company, employee)); ;
            Assert.Equal("Visit - SetVisitedCompany - Visited Company is null", ex.InnerException.Message);

        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_Employee(Employee employee)
        {
            Visit v;
            Company company = _visitedCompany;
            Visitor visitor = _visitor;
            DateTime startTime = DateTime.Now.AddHours(1);

            //Act
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(1, visitor, company, employee)); ;
            Assert.Equal("Visit - SetVisitedEmployee - Visited Employee is null", ex.InnerException.Message);
        }
        [Fact]
        public void Test_ctor_Invalid_Time_Start_Earlier_Than_Now()
        {
            Visit v;
            Company company = _visitedCompany;
            Visitor visitor = _visitor;
            Employee employee = _employee;
            DateTime startTime = DateTime.Now.AddDays(-1);

            //Act
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(1, visitor, company, employee)); ;
            Assert.Equal("Visit - SetStartTime - Start time is too early", ex.InnerException.Message);
        }

    }
}
