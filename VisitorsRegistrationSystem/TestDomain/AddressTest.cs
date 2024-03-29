﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;

namespace TestDomain
{
    public class AddressTest
    {
        [Fact]
        public void SetCity_valid()
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            address.SetCity("Gent");
            Assert.Equal("Gent", address.City);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetCity_Invalid(string city)
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            Assert.Throws<AddressException>(() => address.SetCity(city));


        }
        [Fact]
        public void SetStreet_valid()
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            address.SetStreet("sleepstraat");
            Assert.Equal("sleepstraat", address.Street);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetStreet_Invalid(string street)
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            Assert.Throws<AddressException>(() => address.SetStreet(street));


        }
        [Fact]
        public void SetId_Valid() 
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            address.setId(1);
            Assert.Equal(1, address.Id);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetId_Invalid(int id)
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            Assert.Throws<AddressException>(() => address.setId(id));
        }

        [Fact]
        public void SetHouseNo_valid()
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            address.SetHouseNo("5");
            Assert.Equal("5", address.HouseNumber);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetHouseNo_Invalid(string houseNo)
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            Assert.Throws<AddressException>(() => address.SetHouseNo(houseNo));


        }
        
        [Fact]
        public void SetBusNo_valid()
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", "502");
            address.SetBusNo("505");
            Assert.Equal("505", address.BusNumber);
        }
       
        [Fact]
        public void SetPostalCode_valid()
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            address.SetPostalCode("9000");
            Assert.Equal("9000", address.PostalCode);
        }
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void SetPostalCode_Invalid(string postalCode)
        {
            Address address = new Address("Gent", "9000", "sleepstraat", "5", null);
            Assert.Throws<AddressException>(() => address.SetPostalCode(postalCode));
        }
    }
}