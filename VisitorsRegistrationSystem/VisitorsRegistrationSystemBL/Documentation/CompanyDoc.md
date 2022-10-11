# Documentation: Company
A class made to model a real life company in code. The `Company` class stores the necessary attributes to instantiate an object of type `Company`.

## Attributes
* Name (string): the name of the company
* VATNumber (string): the company's VAT number
* Address (Address): the address of the company
* TelephoneNumber (string): the telephone number of the company
* Email (string): the e-mail address of the company

## Methods
This class only has methods which set its attributes (setters) and constructors.

### Constructor
The constructor has 1 overload.
* Constructor 1: Company(string name, string vat, Address address, string telephoneNumber, string email) : Company. It sets the values of all corresponding attributes of the class depending on the arguments passed in.
* Constructor 2: Company(string name, string vat, string email) : Company. It sets the values of only the core attributes of the class: Name, VATNumber and Email.

### Setters
* SetName(string name) : void. Sets the Name attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetVATNo(string vatNum) : void. Sets the VATNumber attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetAddress(Address a) : void. Sets the Address attribute. Throws an exception if the argument is `null`.
* SetTelNo(string telNo) : void. Sets the TelephoneNumber attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetEmail(string email) : void. Sets the TelephoneNumber attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
