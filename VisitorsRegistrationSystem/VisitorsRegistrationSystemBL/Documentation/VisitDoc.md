# Documentation: Visit
A class made to model a company vistit incl. all the appointment informatie. The 'Visit' class stores the necessary attributes to instantiate an object of type 'Visit'.

## Attributes
* Visitor (Visitor object): an object that refers the visitor
* VisitedCompany (Company object): an object that refers the the visited Company
* VisitedEmployee (Employee object): an object that refers the the visited Employee
* StartTime (DateTime): the starttime of the visit.
* EndTime (DateTime): The endtime of the visit.

## Methods
This class only has methods which set its attributes (setters) and constructors.

### Constructor
Constructor: Visit(Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime, DateTime endTime) : It sets the values of all corresponding attributes of the class depending on the arguments passed in.

### Setters
* VisitorSet(Visitor visitor) : void. Sets the Visitor attribute. Throws an exception if the argument is `null`.
* VisitCompanySet(Company visitedCompany) : void. Sets the Visitor attribute. Throws an exception if the argument is `null`.
* VisitEmployeeSet(Employee visitedEmployee) : void. Sets the Visitor attribute. Throws an exception if the argument is `null`.
* TimeSet(DateTime startTime, DateTime endTime) : void. Sets the Email attribute. Throws an exception if the argument is if starttime is earlier than now or if starttime comes before endtime.