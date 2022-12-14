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
    public class ParkingContractTest
    {
        [Fact]
        public void ParkingContractID_Valid()
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest","XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
            Assert.Equal(1, parkingContract.ID);
        }
        [Theory]
        [InlineData(0)]
        public void ParkingContractID_Invalid(int id)
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
            Assert.Throws<ParkingException>(() => parkingContract.SetID(id));
        }
        [Fact]
        public void ParkingContractCompany_Valid()
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
        }
        //[Theory]
        //[InlineData("")]
        //[InlineData(" ")]
        //public void ParkingContractCompany_Invalid(string company)
        //{
        //    ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1, new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1, 1);
        //    Assert.Throws<VisitorException>(() => parkingContract.SetCompany(company)); ;
        //}
        [Fact]
        public void ParkingContractStartDate_Valid()
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), new DateTime(2022,12,31), DateTime.Now.AddHours(1), 1,1);
            Assert.Equal(new DateTime(2022, 12, 31), parkingContract.StartDate);
        }
        [Theory]
        [InlineData("2017-2-1")]
        public void ParkingContractStartDate_Invalid(DateTime startDate)
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
            Assert.Throws<ParkingException>(() => parkingContract.SetStartDate(startDate));
        }
        [Fact]
        public void ParkingContractEndDate_Valid()
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1, new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), new DateTime(2022, 12, 31), DateTime.Now.AddHours(1), 1,1);
            Assert.Equal(DateTime.Now, parkingContract.EndDate);
        }
        [Theory]
        [InlineData("2017-2-1")]
        public void ParkingContractEndDate_Invalid(DateTime endDate)
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
            Assert.Throws<ParkingException>(() => parkingContract.SetEndDate(endDate));
        }
        [Fact]
        public void ParkingContractReservedSpace_Valid()
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
        }
        [Theory]
        [InlineData(0)]
        public void ParkingContractReservedSpace_Invalid(int reservedSpace)
        {
            ParkingContract parkingContract = ParkingContractFactory.MakeParkingContract(1,new Company("NameTest", "XXXX", "EmailTest@hotmail.com"), DateTime.Now.AddHours(1), DateTime.Now.AddHours(1), 1,1);
            Assert.Throws<ParkingException>(() => parkingContract.SetReservedSpace(reservedSpace));
        }
    }
}
