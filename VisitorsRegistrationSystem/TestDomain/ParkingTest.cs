using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;

namespace TestDomain
{
    public class ParkingTest
    {
        [Fact]
        public void ParkingId_Valid()
        {
            Parking parking = ParkingFactory.MakeParking(1, 1, false, null, null, 1);
            Assert.Equal(1, parking.ID);
        }
        [Theory]
        [InlineData(0)]
        public void ParkingID_Invalid(int id)
        {
            Parking parking = ParkingFactory.MakeParking(1, 1, false, null, null, 1);
            Assert.Throws<ParkingException>(() => parking.SetID(id));
        }

    }
}
