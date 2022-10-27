// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!");
// !!! vergeet connectionstring niet aan te passen !!!
string connectionString = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
CompanyRepositoryADO cRepo = new CompanyRepositoryADO(connectionString);
Company company = CompanyFactory.MakeCompany(null,"Brightest","1234567890",new Address("Elsegem","Kouterlos","2"),"0479564641","arnovantieghem@gmail.com");

Console.WriteLine(cRepo.CompanyExistsInDB(company));