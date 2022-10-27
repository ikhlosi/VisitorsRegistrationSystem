# Documentation: Company
A class made to manage `Company` objects. It allows for communication with the datalayer through the interface `ICompanyRepository`. The `CompanyManager` has CRUD methods for the management of `Company` objects.

## Attributes
* _repo (ICompanyRepository): a private `ICompanyRepository` attribute which is used to delegate methods to the datalayer.

## Methods
This class mostly has CRUD methods.

### Constructor
The constructor simply sets the `_repo` attribute.
* CompanyManager(ICompanyRepository repo) : CompanyManager. It sets the value for the private `_repo` attribute.

### Company management
CRUD methods to manage `Company` objects.
* AddCompany(Company company) : void. Invokes the `WriteCompanyInDB(Company company)` method from `_repo` and passes its argument as an argument to that method. Throws an exception of type `CompanyException` in the following cases:
    * if the argument is `null`;
    * if the argument already exists in the database.
* RemoveCompany(Company company) : void. Invokes the `RemoveCompanyFromDB(Company company)` method from `_repo` and passes its argument as an argument to that method. Throws an exception of type `CompanyException` in the following cases:
    * if the argument is `null`;
    * if the argument does not existsin the database.
* UpdateCompany(Company company) : void. Invokes the `UpdateCompanyInDB(Company company)` method from `_repo` and passes its argument as an argument to that method. Throws an exception of type `CompanyException` in the following cases:
    * if the argument is `null`;
    * if the argument does not existsin the database;
    * if the argument has no new attributes to update.

