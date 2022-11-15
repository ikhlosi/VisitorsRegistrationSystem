// See https://aka.ms/new-console-template for more information
//using ConsoleTestingCompanyDL;
using TestingMySQLConnection;
using VisitorsRegistrationSystemBL.Managers;
using VisitorsRegistrationSystemDL.Repositories;

Console.WriteLine("Hello, World!");

DotNetEnv.Env.TraversePath().Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DB");
Console.WriteLine(connectionString);

//CompanyManager CM = new CompanyManager(new CompanyRepositoryADO(connectionString));

//TESTcompanyRepository repo = new TESTcompanyRepository(connectionString);
//var companies = repo.GetCompaniesFromDB();

//foreach (var item in companies) {
//    Console.WriteLine(item);
//}

MySql.Data.MySqlClient.MySqlConnection conn;
string myConnectionString;

myConnectionString = "server=localhost:3307;uid=root;" +
    "pwd=graduaatsproef;database=VisitorsRegistrationSystem";

try
{
    conn = new MySql.Data.MySqlClient.MySqlConnection();
    conn.ConnectionString = myConnectionString;
    conn.Open();
}
catch (MySql.Data.MySqlClient.MySqlException ex)
{
    throw;
}
