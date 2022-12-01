delete from Visit;
delete from Visitor;
delete from Employee;
delete from Company;
delete from Address;

INSERT INTO Address(id,street,houseNr,bus,postalCode,city,country,visible) VALUES(1,"Kouterlos","2","a","9790","Elsegem","België",1);
INSERT INTO Address(id,street,houseNr,bus,postalCode,city,country,visible) VALUES(2,"Kerkstraat","34",null,"9000","Gent","België",1);
INSERT INTO Address(id,street,houseNr,bus,postalCode,city,country,visible) VALUES(3,"Invisiblestraat","1",null,"9700","Oudenaarde","België",0);
INSERT INTO Address(id,street,houseNr,bus,postalCode,city,country,visible) VALUES(4,"Englishstreet","44","1.001","EN75015","London","England",1);
INSERT INTO Address(id,street,houseNr,bus,postalCode,city,country,visible) VALUES(5,"Kouterlos","2","a","9790","Elsegem","België",1);

INSERT INTO Company(id,name,VAT,email,telNr,addressId,visible) VALUES(1,"Brightest","BE0123456789","brightest@outlook.com","+32479564641",1,1);
INSERT INTO Company(id,name,VAT,email,telNr,addressId,visible) VALUES(2,"De Kerk","BE1234567890","dekerk@jesus.com","+32479564643",2,1);
INSERT INTO Company(id,name,VAT,email,telNr,addressId,visible) VALUES(3,"InvisibleCo","BE0000000000","invisible@nothing.com","+32000000001",3,0);
INSERT INTO Company(id,name,VAT,email,telNr,addressId,visible) VALUES(4,"HoGent","BE0123456789","hogent@hogent.com","+32479564641",5,1);
INSERT INTO Company(id,name,VAT,email,telNr,addressId,visible) VALUES(5,"Google","EN0123456789","google@gmail.com","+32479564645",4,1);

INSERT INTO Employee(id,firstName,lastName,email,occupation,companyId,visible) VALUES(1,"Arno","Vantieghem","arnovantieghem@gmail.com","Tester",1,1);
INSERT INTO Employee(id,firstName,lastName,email,occupation,companyId,visible) VALUES(2,"Tobias","Wille","tw@gmail.com","Dev",4,1);
INSERT INTO Employee(id,firstName,lastName,email,occupation,companyId,visible) VALUES(3,"Ibrahim","Khlosi","ik@gmail.com","CEO",5,1);
INSERT INTO Employee(id,firstName,lastName,email,occupation,companyId,visible) VALUES(4,"Petar","Geenideesorry","pg@gmail.com","Designer",2,1);
INSERT INTO Employee(id,firstName,lastName,email,occupation,companyId,visible) VALUES(5,"In","Visible","iv@blind.com","Relaxing",3,0);

INSERT INTO Visitor(id,name,email,visitorCompany,visible) VALUES(1,"Eliud Kipchoge","eliud@nike.com","NikeRunning",1);
INSERT INTO Visitor(id,name,email,visitorCompany,visible) VALUES(2,"Michael Jackson","mj@gmail.com","Jackson5",0);
INSERT INTO Visitor(id,name,email,visitorCompany,visible) VALUES(3,"Romelu Lukaku","romelu@bueno.com","Duivels",1);
INSERT INTO Visitor(id,name,email,visitorCompany,visible) VALUES(4,"Wout Van Aert","wva@jumbo.com","Jumbo Visma",1);
INSERT INTO Visitor(id,name,email,visitorCompany,visible) VALUES(5,"Kilian Jornet","kiljor@ultra.com","NNormal",1);

INSERT INTO Visit(visitorId,startTime,endTime,companyId,employeeId,visitId,visible) VALUES(1,"2022-11-28 10:10:10","2022-11-28 11:11:11",2,4,1,1);
INSERT INTO Visit(visitorId,startTime,endTime,companyId,employeeId,visitId,visible) VALUES(1,"2022-11-29 10:10:10","2022-11-29 11:11:11",3,5,2,1);
INSERT INTO Visit(visitorId,startTime,endTime,companyId,employeeId,visitId,visible) VALUES(1,"2022-11-30 10:10:10","2022-11-30 11:11:11",4,3,3,0);
INSERT INTO Visit(visitorId,startTime,endTime,companyId,employeeId,visitId,visible) VALUES(4,"2022-12-01 10:10:10","2022-12-01 11:11:11",1,1,4,1);
INSERT INTO Visit(visitorId,startTime,endTime,companyId,employeeId,visitId,visible) VALUES(2,"2022-12-02 10:10:10","2022-12-02 11:11:11",1,1,5,1);
