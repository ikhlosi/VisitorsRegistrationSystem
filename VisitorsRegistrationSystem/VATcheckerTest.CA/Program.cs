// See https://aka.ms/new-console-template for more information
using VisitorsRegistrationSystemBL.Checkers;

Console.WriteLine("Hello, World!");

string vat = "BE0400378486";

if (VATChecker.IsValid(vat))
    Console.WriteLine("valid");
else
    Console.WriteLine("invalid");
