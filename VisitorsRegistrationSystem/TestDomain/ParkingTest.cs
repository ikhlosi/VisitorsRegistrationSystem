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
        [Fact]
        public void SetID_Success()
        {
            Parking parkingA = ParkingFactory.MakeParking(1, 1, false, null, null, 1);

            parkingA.SetID(2);

            Assert.Equal(2, parkingA.ID);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetID_ParkingException(int id)
        {
            Parking parkingA = ParkingFactory.MakeParking(1, 1, false, null, null, 1);

            Assert.Throws<ParkingException>(() => parkingA.SetID(id));

        }
        [Fact]
        public void SetTotalSpaces_Succes() 
        {
            Parking parking = ParkingFactory.MakeParking(1, 1, false, null, null, 1);
            parking.SetTotalSpaces(1);
            Assert.Equal(1, parking.TotalSpaces);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetTotalSpaces_ParkingException(int id) 
        {
            Parking parking = ParkingFactory.MakeParking(1, 1, false, null, null, 1);
            Assert.Throws<ParkingException>(() => parking.SetTotalSpaces(id));
        }
        




    }
}
