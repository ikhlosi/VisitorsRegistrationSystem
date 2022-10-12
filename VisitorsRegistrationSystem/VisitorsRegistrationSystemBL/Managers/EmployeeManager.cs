using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Managers
{
    internal class EmployeeManager
    {
        private Dictionary<string, Employee> _employees = new Dictionary<string, Employee>();

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new VisitorException("EmployeeManager - Addemployee - employee is null");
            if (_employees.ContainsKey(employee.Name)) throw new VisitorException("EmManager - Addemployee - employee has already been registered");
            _employees.Add(employee.Name, employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            if (employee == null) throw new VisitorException("EmployeeManager - DeleteEmployee - visitor is null");
            if (!_employees.ContainsKey(employee.Name)) throw new VisitorException(" EmployeeManager - DeleteEmployee - employee is not registered");
            _employees.Remove(employee.Name);
        }
        public void UpdateEmployee(Employee employee)
        {
            if (employee == null) throw new VisitorException("VisitorManager - UpdateVisitor - visitor is null");
            if (!_employees.ContainsKey(employee.Name)) throw new VisitorException("VisitorManager - UpdateVisitor - visitor is not registered");
            if (_employees[employee.Name].Equals(employee)) throw new VisitorException("VisitorManager - UpdateVisitor - updated visitor is unchanged");
            _employees[employee.Name] = employee;
        }
        public IReadOnlyList<Employee> GetEmployees()
        {
            return _employees.Values.ToList().AsReadOnly();
        }
    }
}
