using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces
{
    public interface IEmployeeRepository {
        bool EmployeeExistsInDB(Employee employee);
        bool EmployeeExistsInDB(int iD);
        Employee GetEmployee(int iD);
        IReadOnlyList<Employee> GetEmployeesFromDB();
        void RemoveEmployeeFromDB(int iD);
        void UpdateEmployeeInDB(Employee employee);
        void WriteEmployeeInDB(Employee employee);
    }
}
