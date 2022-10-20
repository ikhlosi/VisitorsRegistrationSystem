using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using Xunit;
using Xunit.Sdk;
using VisitorsRegistrationSystemBL.Exceptions;

namespace TestDomain
{
    public class EmployeeTest
    {
        [Fact]
        public void EmployeeId_Valid()
        {
            Employee employee = EmployeeFactory.MakeEmployee(1,"Arno","Vantieghem","arnovantieghem@gmail.com","tester");
            Assert.Equal(1, employee.ID);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void EmployeeId_Invalid(int id)
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Throws<EmployeeException>(() => employee.SetId(id));
        }
        [Fact]
        public void EmployeeName_Valid()
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Equal("Arno", employee.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void EmployeeName_Invalid(string name)
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Throws<EmployeeException>(() => employee.SetName(name));
        }
        [Fact]
        public void EmployeeLastname_Valid()
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Equal("Vantieghem", employee.LastName);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void EmployeeLastName_Invalid(string lastName)
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Throws<EmployeeException>(() => employee.SetLastName(lastName));
        }
        [Fact]
        public void EmployeeEmail_Valid()
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Equal("arnovantieghem@gmail.com", employee.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void EmployeeEmail_Invalid(string email)
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Throws<EmployeeException>(() => employee.SetEmail(email));
        }
        [Fact]
        public void EmployeeFunction_Valid()
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Equal("tester", employee.Function);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void EmployeeFunction_invalid(string function)
        {
            Employee employee = EmployeeFactory.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Throws<EmployeeException>(() => employee.SetFunction(function));
        }
    }
}
