// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!");
// !!! vergeet connectionstring niet aan te passen !!!
string connectionStringArno = @"Data Source=laptop-hfkukp2u\sqlexpress;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
string connectionStringTobias = @"Data Source=DESKTOP-91NOPN3\SQLEXPRESS;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
string connectionStringIbra = @"Data Source=DESKTOP-QT687QR\SQLEXPRESS;Initial Catalog=VisitorsRegistrationSystem;Integrated Security=True";
string connectionStringPetar = null;
CompanyRepositoryADO cRepo = new CompanyRepositoryADO(connectionStringArno);
Company company = CompanyFactory.MakeCompany(null,"Brightest","1234567890",new Address("Elsegem","Kouterlos","2"),"0479564641","arnovantieghem@gmail.com");

Console.WriteLine(cRepo.CompanyExistsInDB(company));