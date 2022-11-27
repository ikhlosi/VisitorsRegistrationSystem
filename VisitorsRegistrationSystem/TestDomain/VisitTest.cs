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
        private Company _company;
        private Visitor _visitor;
        private Employee _employee;
        
        public VisitTest()
        {
            this._company = CompanyFactory.MakeCompany(1, "CompanyA", "XXXXXXX", new Address("Gent", "Sleepstraat", "5", "501"), "0471970499", "CompanyA@hotmail.com");
            this._visitor = VisitorFactory.MakeVisitor(null, "jos", "jos@hotmail.com", "CompanyV");
            this._employee = EmployeeFactory.MakeEmployee(1, "John", "Doe", "johndoe@hotmail.com", "TeamLead");
        }
        //public Visit(Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime)
        // TODO setten van endtime omdat we dit niet meer doen in constructor
        [Fact]
        public void Test_ctor_valid()
        {
            //Arrange
            Company company = _company;
            Visitor visitor = _visitor;
            Employee employee = _employee;
            DateTime startTime = DateTime.Now;
          
            //Act 
            Visit visit = VisitFactory.MakeVisit(1,_visitor, _company, _employee, startTime);

            //Assert
            Assert.Equal(_visitor,visit.Visitor);
            Assert.Equal(_company, visit.VisitedCompany);
            Assert.Equal(_employee, visit.VisitedEmployee);
            Assert.Equal(startTime, visit.StartTime);
       

        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_VisitorSet(Visitor visitor)
        {
            //Arrange
            Visit v;
            Company company = new Company("CompanyA", "XXXXXXX", "CompanyA@hotmail.com");
            Employee employee = new Employee("John", "Doe", "TeamLead");
            DateTime startTime = DateTime.Now;
            //Assert
            var ex = Assert.Throws<VisitException>(() => v =  VisitFactory.MakeVisit(null,visitor,company,employee,startTime));;
            Assert.Equal("Visit - Visitor is null", ex.Message);
        }
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_CompanySet(Company company)
        {
            //Arrange 
            Visit v;
            Visitor visitor = VisitorFactory.MakeVisitor(null,"Tobias", "tobiaswille@hotmail.com",null);
            Employee employee = new Employee("John", "Doe", "TeamLead");

            DateTime startTime = DateTime.Now;
         
            //Act 
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(null,visitor,company,employee,startTime)); ;
            Assert.Equal("Visit - Visited Company is null", ex.Message);

        }
        3
        [Theory]
        [InlineData(null)]
        public void Test_ctor_Invalid_Employee(Employee employee)
        {
            Visit v;
            Company company = new Company("CompanyA", "XXXXXXX", "CompanyA@hotmail.com");
            Visitor visitor = VisitorFactory.MakeVisitor(null,"Tobias", "Tobias@hotmail.com",null);
            DateTime startTime = DateTime.Now;
          
            //Act
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(null,visitor, company, employee, startTime)); ;
            Assert.Equal("Visit - Visited Employee is null", ex.Message);
        }
        [Fact]
        public void Test_ctor_Invalid_Time_Start_Later_Than_Now()
        {
            Visit v;
            Company company = new Company("CompanyA", "XXXXXXX", "CompanyA@hotmail.com");
            Visitor visitor = VisitorFactory.MakeVisitor(null,"Tobias", "Tobias@hotmail.com",null);
            Employee employee = new Employee("Arno", "Vantieghem", "Tester");
            DateTime startTime = DateTime.Now.AddDays(1);
         
            //Act
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(null,visitor, company, employee, startTime)); ;
            Assert.Equal("Visit - Start time is too late", ex.Message);
        }
        [Fact]
        public void Test_ctor_Invalid_Time_Start_Later_Than_End()
        {
            Visit v;
            Company company = new Company("CompanyA", "XXXXXXX", "CompanyA@hotmail.com");
            Visitor visitor = VisitorFactory.MakeVisitor(null,"Tobias", "Tobias@hotmail.com",null);
            Employee employee = new Employee("Arno", "Vantieghem", "Tester");
            DateTime startTime = DateTime.MinValue.AddHours(2);
       
            //Act
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = VisitFactory.MakeVisit(null,visitor, company, employee, startTime)); ;
            Assert.Equal("Visit - End time earlier than Start time", ex.Message);
        }
    }
}
