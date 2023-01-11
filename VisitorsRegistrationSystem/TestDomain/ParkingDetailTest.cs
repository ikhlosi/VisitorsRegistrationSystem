using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias","xxxxxx","test@email.com"), 1);
            parkingdetail.SetID(2);
            Assert.Equal(2, parkingdetail.ID);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetID_ParkingException(int id)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetID(id));
        }
        [Fact]
        public void SetStartTime_succes()
        {
            DateTime startTime = DateTime.Now.AddHours(1);
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            parkingdetail.SetStartTime(startTime);
            Assert.Equal(startTime, parkingdetail.StartTime);
        }
        

        [Fact]
        public void SetEndTime_succes()
        {
            DateTime endtime = DateTime.Now.AddHours(1);
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now, DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            parkingdetail.SetEndTime(endtime);
            Assert.NotNull(parkingdetail.EndTime);
            Assert.Equal(endtime, parkingdetail.EndTime);
        }
     

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void SetEndTime_ParkingException(int hours)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetEndTime(DateTime.Now.AddHours(-hours)));
        }
        [Fact]
        public void SetLicensePlate_succes()
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            parkingdetail.SetLicensePlate("007-BND");
            Assert.Equal("007-BND", parkingdetail.LicensePlate);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetLicensePlate_ParkingException(string licensePlate)
        {
            ParkingDetail parkingdetail = ParkingDetailFactory.MakeParkingDetail(1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), "007-BND", new Company("tobias", "xxxxxx", "test@email.com"), 1);
            Assert.Throws<ParkingException>(() => parkingdetail.SetLicensePlate(licensePlate));
        }
    }
}
