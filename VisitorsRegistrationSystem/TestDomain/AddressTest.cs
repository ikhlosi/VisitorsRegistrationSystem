using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;

namespace TestDomain
{
    public class AddressTest
    {
        [Fact]
        public void SetCit_valid()
        {
            Address address = new Address("Gent","sleepstraat","5",null);
            address.SetCity("Gent");
            Assert.Equal("Gent", address.City);
        }
        [Fact]
        public void SetStreet_valid()
        {

        }
    }
}
