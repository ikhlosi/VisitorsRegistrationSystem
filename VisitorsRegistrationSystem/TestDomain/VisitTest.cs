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
    public class VisitTest
    {
        [Fact]
        public void Test_ctor_valid()
        {
            //Arrange
            Company company = new Company("CompanyA","XXXXXXX","CompanyA@hotmail.com");
            Visitor visitor = new Visitor("Tobias","Tobias@hotmail.com");
            Employee employee = new Employee("John","Doe","TeamLead");
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddHours(1);

            //Act 
            Visit visit = new Visit(visitor,company,employee,startTime,endTime);

            //Assert
            Assert.Equal(visitor,visit.Visitor);
            Assert.Equal(company, visit.VisitedCompany);
            Assert.Equal(employee, visit.VisitedEmployee);
            Assert.Equal(startTime, visit.StartTime);
            Assert.Equal(endTime, visit.EndTime);

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
            DateTime endTime = DateTime.Now.AddHours(1);
            //Act 
            Visit visit = new Visit(visitor, company, employee, startTime, endTime);
            //Assert
            var ex = Assert.Throws<VisitException>(() => v = new Visit(visitor,company,employee,startTime,endTime));;
            Assert.Equal("Visit - Visitor is null", ex.Message);
        }
        //[Theory]
        //[InlineData(null)]
        //public void Test_ctor_Invalid_CompanySet(Company company)
        //{
        //    //Arrange 
        //    Company c;
        //    Visitor visist = new Visitor("Tobias","tobiaswille@hotmail.com");
        //    Employee employee = new Employee("John", "Doe", "TeamLead");
        //    DateTime startTime = DateTime.Now;
        //    DateTime endTime = DateTime.Now.AddHours(1);
        //    //Act 
        //    Company company = new Company();
        //    //Assert
        //    var ex = Assert.Throws<CompanyException>(() => c = new Company()); ;
        //    Assert.Equal("Visit - Visited Company is null", ex.Message);

        //}
        //[Theory]
        //[InlineData(null)]
        //public void Test_ctor_Invalid_Employee(Employee employee)
        //{
        //    Employee c;
        //    Company company = new Company("CompanyA", "XXXXXXX", "CompanyA@hotmail.com");
        //    Visitor visitor = new Visitor("Tobias", "Tobias@hotmail.com");
        //    DateTime startTime = DateTime.Now;
        //    DateTime endTime = DateTime.Now.AddHours(1);
        //    //Act
        //    Employee employee = new Employee(name, lastName, function);
        //    //Assert
             
        //}
    }
}
