using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class EmployeeManager
    {
        // private Dictionary<string, Employee> _employees = new Dictionary<string, Employee>();

        private ICompanyRepository _repo;

        public EmployeeManager(ICompanyRepository repo) {
            _repo = repo;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new EmployeeException("EmployeeManager - Addemployee - employee is null");
            try {
                // if (_repo.EmployeeExistsInDB(employee)) throw new EmployeeException("EmployeeManager - AddEmployee - employee already exists in DB.");
                // this check will be done in UI, through a pop-up windows asking if the user is sure he wants to add 
                // 2 employees with the same name
                _repo.WriteEmployeeInDB(employee);
            }
            catch (Exception ex) {
                throw new EmployeeException("EmployeeManager - AddEmployee", ex);
            }
        }
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null) throw new EmployeeException("EmployeeManager - RemoveEmployee - employee is null");
            try {
                if (!_repo.EmployeeExistsInDB(employee.ID)) throw new EmployeeException("EmployeeManager - RemoveEmployee - employee does not exist in DB.");
                _repo.RemoveEmployeeFromDB(employee.ID);
            }
            catch (Exception ex) {
                throw new EmployeeException("EmployeeManager - RemoveEmployee", ex);
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            if (employee == null) throw new EmployeeException("EmployeeManager - UpdateEmployee - employee is null");
            try {
                if (!_repo.EmployeeExistsInDB(employee.ID)) throw new EmployeeException("EmployeeManager - UpdateEmployee - employee does not exist in DB.");
                Employee employeeDb = _repo.GetEmployee(employee.ID);
                if (employeeDb.IsSame(employee)) throw new EmployeeException("EmployeeManager - UpdateEmployee - fields are the same, nothing to update.");
                _repo.UpdateEmployeeInDB(employee);
            }
            catch (Exception ex) {
                throw new EmployeeException("EmployeeManager - UpdateEmployee", ex);
            }
        }
        public IReadOnlyList<Employee> GetEmployees()
        {
            try {
                return _repo.GetEmployeesFromDB();
            }
            catch (Exception ex) {
                throw new EmployeeException("EmployeeManager - GetEmployees", ex);
            }
        }
    }
}
