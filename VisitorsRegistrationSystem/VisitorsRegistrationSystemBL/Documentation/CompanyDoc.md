# Documentation: Company
A class made to model a real life company in code. The `Company` class stores the necessary attributes to instantiate an object of type `Company`.

## Attributes
* Name (string): the name of the company
* VATNumber (string): the company's VAT number
* Address (Address): the address of the company
* TelephoneNumber (string): the telephone number of the company
* Email (string): the e-mail address of the company
* _employees (List<Company>): a list which stores the employees of the company

## Methods
This class mostly has methods which set its attributes (setters). Furthermore, it has a constructor and methods to add employees to the _employees attribute.

### Constructor
The constructor only sets the core attributes; those that are absolutely needed for a Company object to exist. These attributes are: Name, VATNumber and Email. The constructor's visibility is set to `internal`, such that it can only be invoked from within the library.
* Company(string name, string vATNumber, string email) : Company. It sets the values of only the core attributes of the class: Name, VATNumber and Email.

The static class: CompanyFactory is the class responsible for instantiating an object of type Company. Non-core attributes of Company will be set by CompanyFactory as well, if provided as arguments.

### Setters
* SetID(int id) : void. Sets the ID attribute. Throws an exception if the argument is less than or equal to 0.
* SetName(string name) : void. Sets the Name attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetVATNo(string vatNum) : void. Sets the VATNumber attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetAddress(Address a) : void. Sets the Address attribute. Throws an exception if the argument is `null`.
* SetTelNo(string telNo) : void. Sets the TelephoneNumber attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetEmail(string email) : void. Sets the Email attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.

### Employee management
The below methods is implemented to add Employees to the list of employees.
* AddEmployee(Employee employee) : void. Adds an object of type `Employee` to `_employees`.

### Methods to compare Company objects
* IsSame(Company otherCompany) : bool. Returns true if argument has the same values for all the respective attributes as current object. The specific attributes that are checked are: ID, Name, VATNumber, Address, TelephoneNumber and Email. Returns false otherwise.
* Equals() & GetHashCode() override: to compare 2 Company objects with each other. Returns true if the ID is the same.
