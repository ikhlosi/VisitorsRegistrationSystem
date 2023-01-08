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
    public class ParkingDetailTest
    {
        [Fact]
        public void SetID_succes()
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            parkingdetail.SetID(2);
            Assert.Equal(2, parkingdetail.ID);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetID_ParkingException(int id)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetID(id));
        }
        [Fact]
        public void SetStartTime_succes()
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            parkingdetail.SetStartTime(DateTime.Now.AddHours(1));
            Assert.Equal(DateTime.Now.AddHours(1), parkingdetail.StartTime);
        }
        [Theory]
        [InlineData(null)]
        public void SetStartTime_ParkingException(DateTime? startTime)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetStartTime(DateTime.Now.AddHours(1)));
        }



        [Fact]
        public void SetEndTime_succes()
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now, DateTime.Now.AddHours(1), "007-BND", 1, 1);
            parkingdetail.SetEndTime(DateTime.Now);
            Assert.Equal(DateTime.Now, parkingdetail.EndTime);
        }
     

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void SetEndTime_ParkingException(int hours)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetEndTime(DateTime.Now.AddHours(-hours)));
        }
        [Fact]
        public void SetLicensePlate_succes()
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            parkingdetail.SetLicensePlate("007-BND");
            Assert.Equal("007-BND", parkingdetail.LicensePlate);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetLicensePlate_ParkingException(string licensePlate)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", 1, 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetLicensePlate("007-BND"));
        }
    }
}
