// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!\n");
// !!! vergeet connectionstring niet aan te passen !!!
//string connectionStringArno = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
DotNetEnv.Env.TraversePath().Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
Console.WriteLine(connectionString); // for debug
CompanyRepositoryADO cRepo = new CompanyRepositoryADO(connectionString);
<<<<<<< HEAD
Company company = CompanyFactory.MakeCompany(null,"Brightest","1234567890",new Address("Elsegem","9000","Kouterlos","2", null),"0479564641","arnovantieghem@gmail.com");
Company companyFalse = CompanyFactory.MakeCompany(null, "Brightest", "2234567890", new Address("Elsegem", "9000", "Kouterlos", "2",null), "0479564641", "arnovantieghem@gmail.com");
=======
Company company = CompanyFactory.MakeCompany(null,"Brightest","1234567890",new Address("Elsegem", "9790", "Kouterlos","2", null),"0479564641","arnovantieghem@gmail.com");
Company companyFalse = CompanyFactory.MakeCompany(null, "Brightest", "2234567890", new Address("Elsegem", "9790", "Kouterlos", "2",null), "0479564641", "arnovantieghem@gmail.com");
>>>>>>> main

Console.WriteLine("Company Exists: \n");

Console.WriteLine("Moet true zijn: " + cRepo.CompanyExistsInDB(company));
Console.WriteLine("Moet false zijn: " + cRepo.CompanyExistsInDB(companyFalse));
Console.WriteLine("Moet true zijn: " + cRepo.CompanyExistsInDB(2));
Console.WriteLine("Moet false zijn: " + cRepo.CompanyExistsInDB(99));

Console.WriteLine("Get all companies: \n");

foreach(var i in cRepo.GetCompaniesFromDB())
{
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("Get company with id 1:\n\n\t" + cRepo.GetCompanyByIdFromDB(1).ToString());

Console.WriteLine("\nGet all companies with Name: Brightest : \n");

foreach (var i in cRepo.GetCompaniesByNameFromDB("Brightest")) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nGet all companies with Vat: 1234567890 : \n");

foreach (var i in cRepo.GetCompaniesByVatnumFromDB("1234567890")) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nGet all companies with Telnr: +32479564643 : \n");

foreach (var i in cRepo.GetCompaniesByTelnrFromDB("+32479564643")) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nGet all companies with Email: brightest@bright.com : \n");

foreach (var i in cRepo.GetCompaniesByEmailFromDB("brightest@bright.com")) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nGet all companies with Address: Street: Kouterlos HouseNr: 2a bus: NULL city: Elsegem : \n");
<<<<<<< HEAD
Address address = new Address("Elsegem", "9000", "Kouterlos", "2A", null);
=======
Address address = new Address("Elsegem", "9790", "Kouterlos", "2A", null);
>>>>>>> main
foreach (var i in cRepo.GetCompaniesByAddressFromDB(address)) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nGet all companies with Address: Street: Kouterlos HouseNr: 2a bus: 1.001 city: Elsegem : \n");
<<<<<<< HEAD
Address addressWithBus = new Address("Elsegem", "9000", "Kouterlos", "2A", "1.001");
=======
Address addressWithBus = new Address("Elsegem", "9790", "Kouterlos", "2A", "1.001");
>>>>>>> main
foreach (var i in cRepo.GetCompaniesByAddressFromDB(addressWithBus)) {
    Console.WriteLine("\t" + i.ToString());
}

Console.WriteLine("\nDeleting company with id: 11");
cRepo.RemoveCompanyFromDB(11);
Console.WriteLine("Company 11 has been deleted!");

Console.WriteLine("\nUpdating company with id: 5");
<<<<<<< HEAD
Address address1 = new Address(1, "Elsegem","9000", "Kouterlos", "2a", null);
=======
Address address1 = new Address(1, "Elsegem", "9790", "Kouterlos", "2a", null);
>>>>>>> main
Company company1 = CompanyFactory.MakeCompany(5, "Updated", "1234567890", address1, "+32479564643", "updated@gmail.com");
cRepo.UpdateCompanyInDB(company1);
Console.WriteLine("Company 5 has been updated!");

Company company2 = CompanyFactory.MakeCompany(null, "NewCompany", "1234567890", address1, "+32479564642", "new@gmail.com");
Console.WriteLine("\nAdding company to DB");
cRepo.WriteCompanyInDB(company2);
Console.WriteLine("Company NewCompany has been added to the DB");