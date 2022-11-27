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

namespace TestDomain
{
    public class VisitorTest {

        [Fact]
        public void VisitorName_Valid()
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1,"Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Equal("Tobias", visitor.Name); ;
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void VisitorName_Invalid(string name)
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Throws<VisitorException>(() => visitor.SetName(name));
        }
        [Fact]
        public void VisitorEmail_Valid()
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Equal("tobiaswille@hotmail.com", visitor.Email);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void VisitorEmail_Invalid(string email)
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Throws<VisitorException>(() => visitor.SetEmail(email));
        }
        [Fact]
        public void SetVisitorCompany_Valid()
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Equal("CompanyTest", visitor.VisitorCompany);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void SetVisitorCompany_Invalid(string company)
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Throws<VisitorException>(() => visitor.SetVisitorCompany(company));
        }
        [Fact]
        public void SetVisitorId_Valid()
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Equal(1, visitor.Id);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void SetVisitorId_Invalid(string id)
        {
            Visitor visitor = VisitorFactory.MakeVisitor(1, "Tobias", "tobiaswille@hotmail.com", "CompanyTest");
            Assert.Throws<VisitorException>(() => visitor.SetVisitorCompany(id));
        }

    }
}
