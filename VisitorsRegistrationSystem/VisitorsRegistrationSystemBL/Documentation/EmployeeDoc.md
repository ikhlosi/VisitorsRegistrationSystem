# Documentation: Employee
A class made to model a real life employee within a company in code. The 'Employee' class stores the necessary attributes to instantiate an object of type 'Employee'.
## Attributes
*ID(int): the id of an employee
*Name (string): the first name of an employee
*LastName (string): the last name of an employee
*Email (string): the email of an employee
*function (string): the function of an employee withing the company

## Methods
This class only has methods which set its attributes (setters) and constructors.

### Constructor
The constructor only sets the core attributes; those that are absolutely needed for an Employee object to exist. These attributes are: Name, LastName and function. The constructor's visibility is set to `internal`, such that it can only be invoked from within the library.
* Employee(string name, string lastName, string function)

The static class: EmployeeFactory is the class responsible for instantiating an object of type Employee. Non-core attributes of Employee will be set by EmployeeFactory as well, if provided as arguments.

### Setters
* SetName(string name) : void. Sets the Name attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetLastName(string lastname) : void. Sets the LastName attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetEmail(string email) : void. Sets the Email attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetFunction(string function) : void. Sets the Function attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.

### Methods to compare Employee objects
* IsSame(Employee employee) : bool. Returns true if argument has the same values for all the respective attributes as current object. The specific attributes that are checked are: ID, Name, Lastname, Email, Function. Returns false otherwise.
* Equals() & GetHashCode() override: to compare 2 Employee objects with each other. Returns true if the ID is the same.
