INSERT INTO VisitorsRegistrationSystem.Address (street, houseNr, bus, postalCode, city, country)
VALUES
('Kerkstraat', 1, NULL, '9000', 'Gent', 'Belgium'),
('Veldstraat', 1, NULL, '9000', 'Gent', 'Belgium'),
('Voskenslaan', 5, 'B', '8000', 'Brugge', 'Belgium'),
('Meir', 78, NULL, '2000', 'Antwerpen', 'Belgium'),
('Groenstraat', 20, NULL, '3660', 'Oudsbergen', 'Belgium');

INSERT INTO VisitorsRegistrationSystem.Company (name, VAT, email, telNr, addressId)
VALUES
('CompanyA', '1111111111', 'companya@outlook.com', '0411111111', 5),
('CompanyB', '2222222222', 'companyb@outlook.com', '0422222222', 4),
('CompanyC', '3333333333', 'companyc@outlook.com', '0433333333', 3),
('CompanyD', '4444444444', 'companyd@outlook.com', '0444444444', 2),
('CompanyE', '5555555555', 'companye@outlook.com', '0455555555', 1);

INSERT INTO VisitorsRegistrationSystem.Employee (firstName, lastName, email, occupation, companyId)
VALUES
('John', 'Doe', 'john.doe@outlook.com', 'Senior Developer', 3),
('Rick', 'Deschamps', 'rickd@outlook.com', 'Junior Developer', 4),
('Ellen', 'Vandevelde', 'ellen.vdv@outlook.com', 'CEO', 1), -- feminism
('Abdullah', 'Desmet', 'abdu.desmet@outlook.com', 'Data Analyst', 5),
('Ovuvuevuevue', 'Enyetuenwuevue', 'ovuvuevuevue.enyetuenwuevue@outlook.com', 'Server Admin', 1);

-- TO BE CONTINUED
/*
INSERT INTO VisitorsRegistrationSystem.Visit (visitorId, )
VALUES
('John', 'Doe', 'john.doe@outlook.com', 'Senior Developer', 3),
('Rick', 'Deschamps', 'rickd@outlook.com', 'Junior Developer', 4),
('Ellen', 'Vandevelde', 'ellen.vdv@outlook.com', 'CEO', 1), -- feminism
('Abdullah', 'Desmet', 'abdu.desmet@outlook.com', 'Data Analyst', 5),
('Ovuvuevuevue', 'Enyetuenwuevue', 'ovuvuevuevue.enyetuenwuevue@outlook.com', 'Server Admin', 1);

INSERT INTO VisitorsRegistrationSystem.Employee (firstName, lastName, email, occupation, companyId)
VALUES
('John', 'Doe', 'john.doe@outlook.com', 'Senior Developer', 3),
('Rick', 'Deschamps', 'rickd@outlook.com', 'Junior Developer', 4),
('Ellen', 'Vandevelde', 'ellen.vdv@outlook.com', 'CEO', 1), -- feminism
('Abdullah', 'Desmet', 'abdu.desmet@outlook.com', 'Data Analyst', 5),
('Ovuvuevuevue', 'Enyetuenwuevue', 'ovuvuevuevue.enyetuenwuevue@outlook.com', 'Server Admin', 1);
*/
