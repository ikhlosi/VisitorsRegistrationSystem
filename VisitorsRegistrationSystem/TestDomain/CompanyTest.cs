﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using Xunit;
using Xunit.Sdk;


namespace TestDomain
{
    public class CompanyTest
    {
        [Fact]
        public void SetName_RegularInput_Success()
        {
            #region Arrange
            int id = 1;
            string name = "Company A";
            string vATNumber = "XXXXXXXXXXXXXXXXX";
            Address address = new Address("Gent", "Luiklaan", "10");
            int expectedResult = 1;
            Company companyA = CompanyFactory.MakeCompany(id, name, vATNumber, address, null, null);
            #endregion
            #region Act
            companyA
            #endregion
            #region Assert
            Assert.Equal(expectedResult, companyA.ID);
            #endregion
        }





    }
}
