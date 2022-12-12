using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;

namespace TestDomain
{
    public class ParkingTest
    {
        [Fact]
        public void EmployeeId_Valid()
        {
            Parking parking = parking.MakeEmployee(1, "Arno", "Vantieghem", "arnovantieghem@gmail.com", "tester");
            Assert.Equal(1, employee.ID);
        }
    }
}
