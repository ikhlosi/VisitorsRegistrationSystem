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
            Visit visit = VisitFactory.MakeVisit(2, new Visitor("Name", "Name@hotmail.com", "CompanyTest"), new Company("Name", "XXXXXX", "mail@hotmail.com"), new Employee("Name", "LastName", "Function"), DateTime.Now.AddHours(1));
            Assert.Equal(2, visit.Id);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Visit_SetId_Invalid(int id)
        {
            var ex = Assert.Throws<VisitException>(() => VisitFactory.MakeVisit(id, new Visitor("Name", "Name@hotmail.com", "CompanyTest"), new Company("Name", "XXXXXX", "mail@hotmail.com"), new Employee("Name", "LastName", "Function"), DateTime.Now.AddHours(1)));
            Assert.Equal("Visit - SetId - id smaller than 1",ex.InnerException.Message);
        }

    }
}
