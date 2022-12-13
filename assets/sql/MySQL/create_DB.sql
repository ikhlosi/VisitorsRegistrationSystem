CREATE DATABASE VisitorsRegistrationSystem;

USE VisitorsRegistrationSystem;

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
	endTime datetime,
	companyId int NOT NULL,
	employeeId int NOT NULL,
	visitId INT NULL AUTO_INCREMENT,
    CONSTRAINT PK_Visit PRIMARY KEY (visitorId,startTime),
	CONSTRAINT UNIQUE INDEX visitId_UNIQUE (visitId ASC)
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

ALTER TABLE VisitorsRegistrationSystem.Address 
ADD COLUMN visible BIT NULL DEFAULT true;

ALTER TABLE VisitorsRegistrationSystem.Company 
ADD COLUMN visible BIT NULL DEFAULT true;

ALTER TABLE VisitorsRegistrationSystem.Employee 
ADD COLUMN visible BIT NULL DEFAULT true;

ALTER TABLE VisitorsRegistrationSystem.Visit 
ADD COLUMN visible BIT NULL DEFAULT true;

ALTER TABLE VisitorsRegistrationSystem.Visitor 
ADD COLUMN visible BIT NULL DEFAULT true;

CREATE TABLE `parking` (
  `id` int NOT NULL AUTO_INCREMENT,
  `totalSpaces` int DEFAULT '0',
  `occupiedSpaces` int DEFAULT '0',
  `full` bit(1) NOT NULL DEFAULT b'0',
  `visible` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `parkingdetails` (
  `id` int NOT NULL AUTO_INCREMENT,
  `startTime` datetime(2) NOT NULL,
  `endTime` datetime(2) DEFAULT NULL,
  `licensePlate` varchar(45) NOT NULL,
  `visitedCompanyId` int NOT NULL,
  `parkingId` int NOT NULL DEFAULT '1',
  `visible` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `FK_ParkingDetails_Parking_idx` (`parkingId`),
  KEY `FK_ParkingDetails_Company_idx` (`visitedCompanyId`),
  CONSTRAINT `FK_ParkingDetails_Company` FOREIGN KEY (`visitedCompanyId`) REFERENCES `company` (`id`),
  CONSTRAINT `FK_ParkingDetails_Parking` FOREIGN KEY (`parkingId`) REFERENCES `parking` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `parkingcontract` (
  `id` int NOT NULL AUTO_INCREMENT,
  `companyId` int NOT NULL,
  `spaces` int DEFAULT '0',
  `startDate` datetime(2) NOT NULL,
  `endDate` datetime(2) NOT NULL,
  `parkingId` int NOT NULL DEFAULT '1',
  `visible` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `FK_ParkingContract_Company_idx` (`companyId`),
  KEY `FK_ParkingContract_Parking_idx` (`parkingId`),
  CONSTRAINT `FK_ParkingContract_Company` FOREIGN KEY (`companyId`) REFERENCES `company` (`id`),
  CONSTRAINT `FK_ParkingContract_Parking` FOREIGN KEY (`parkingId`) REFERENCES `parking` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
