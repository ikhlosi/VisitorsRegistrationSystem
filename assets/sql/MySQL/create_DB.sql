CREATE DATABASE VisitorsRegistrationSystem;

CREATE TABLE Address (
	id int NOT NULL AUTO_INCREMENT,
	street varchar(250) NOT NULL,
	houseNr varchar(250) NOT NULL,
	bus varchar(250),
	postalCode varchar(250) NOT NULL,
	city varchar(250) NOT NULL,
	country varchar(250) NOT NULL,
    CONSTRAINT PK_Address PRIMARY KEY (id)
);

CREATE TABLE Company (
	id int NOT NULL AUTO_INCREMENT,
	name varchar(250) NOT NULL,
	VAT varchar(250) NOT NULL,
	email varchar(250) NOT NULL,
	telNr varchar(250) NULL,
	addressId int NOT NULL,
    CONSTRAINT PK_Company PRIMARY KEY (id)
);

CREATE TABLE Employee(
	id int NOT NULL AUTO_INCREMENT,
	firstName varchar(250) NOT NULL,
	lastName varchar(250) NOT NULL,
	email varchar(250) NULL,
	occupation varchar(250) NOT NULL,
	companyId int NOT NULL,
    CONSTRAINT PK_Employee PRIMARY KEY (id) 
);


CREATE TABLE Visit(
	visitorId int NOT NULL,
	startTime datetime NOT NULL,
	endTime datetime NOT NULL,
	companyId int NOT NULL,
	employeeId int NOT NULL,
    CONSTRAINT PK_Visit PRIMARY KEY (visitorId,startTime) 
);

CREATE TABLE Visitor(
	id int NOT NULL AUTO_INCREMENT,
	name varchar(250) NOT NULL,
	email varchar(250) NOT NULL,
	visitorCompany varchar(250) NULL,
    CONSTRAINT PK_Visitor PRIMARY KEY (id) 
);

ALTER TABLE Company
ADD CONSTRAINT FK_Company_Address
FOREIGN KEY (addressId) REFERENCES Address (id);

ALTER TABLE Employee
ADD  CONSTRAINT FK_Employee_Company
FOREIGN KEY (companyId) REFERENCES Company (id);

ALTER TABLE Visit
ADD CONSTRAINT FK_Visit_Company
FOREIGN KEY (companyId) REFERENCES Company (id);

ALTER TABLE Visit
ADD  CONSTRAINT FK_Visit_Employee
FOREIGN KEY (employeeId) REFERENCES Employee (id);

ALTER TABLE Visit
ADD  CONSTRAINT FK_Visit_Visitor
FOREIGN KEY (visitorId) REFERENCES Visitor (id);