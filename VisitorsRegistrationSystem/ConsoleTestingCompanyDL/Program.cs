// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!\n");
// !!! vergeet connectionstring niet aan te passen !!!
string connectionString = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
CompanyRepositoryADO cRepo = new CompanyRepositoryADO(connectionString);
Company company = CompanyFactory.MakeCompany(null,"Brightest","1234567890",new Address("Elsegem","Kouterlos","2",null),"0479564641","arnovantieghem@gmail.com");
Company companyFalse = CompanyFactory.MakeCompany(null, "Brightest", "2234567890", new Address("Elsegem", "Kouterlos", "2",null), "0479564641", "arnovantieghem@gmail.com");

Console.WriteLine("Company Exists: \n");

Console.WriteLine("Moet true zijn: " + cRepo.CompanyExistsInDB(company));
Console.WriteLine("Moet false zijn: " + cRepo.CompanyExistsInDB(companyFalse));
Console.WriteLine("Moet true zijn: " + cRepo.CompanyExistsInDB(1));
Console.WriteLine("Moet false zijn: " + cRepo.CompanyExistsInDB(99));

Console.WriteLine("Get all companies: \n");

foreach(var i in cRepo.GetCompaniesFromDB())
{
    Console.WriteLine(i.ToString());
}

