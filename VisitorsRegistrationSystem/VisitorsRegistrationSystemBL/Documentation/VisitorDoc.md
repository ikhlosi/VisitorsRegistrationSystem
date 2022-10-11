# Documentation: Visitor
A class made to model a person that registers as a visitor when visiting a company in code. The `Visitor` class stores the necessary attributes to instantiate an object of type `Visitor`.

## Attributes
* Name (string): the name of the company
* Email (string): the e-mail address of the company
* VisitorCompany (Company object): an object that refers to the company of the visitor

## Methods
This class only has methods which set its attributes (setters) and constructors.

### Constructor
The constructor has 1 overload.
* Constructor 1: Visitor(string name, string email) : Visitor. It sets the values of all corresponding attributes of the class depending on the arguments passed in.
* Constructor 2: Visitor(string name, string email, Company visitorCompany) : Visitor. It sets the values of the base components through the first constructor and adds the VisitorCompany which isn't always required

### Setters
* SetName(string name) : void. Sets the Name attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters.
* SetEmail(string email) : void. Sets the Email attribute. Throws an exception if the argument is `null` or is an empty string or a string with only whitespace characters or when the format doesn't allign with the required format for emailadresses.
* SetVisitorCompany(Company company) : void. Sets the VisitorCompany attribute. Throws an exception if the argument is 'null'.