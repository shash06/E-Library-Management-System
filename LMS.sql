create database library

use library

select * from Document_Details

create table User_Details
(User_id int primary key identity(1000,1),
Name varchar(50),
Address varchar(70),
Phone_Number bigint,
Interest varchar(70),
Email_id varchar(50) unique,
Password varchar(30),
Bank_Name varchar(50),
Credit_Card bigint,
Subscription bit,
Admin bit
);

create table Document_Details
(Doc_id int primary key identity(1000,1),
Title varchar(20),
Description varchar(200),
Discipline_id int foreign key references Discipline(Discipline_Id),
Author varchar(20),
Date date,
Price Numeric(5),
Doc_TypeId int foreign key references Document_Type(Doc_TypeId),
);


Create Table Document_Type
(
	Doc_TypeId int primary key identity(100,1),
	Doc_TypeName varchar(30) not null
);

Create Table Discipline
(
	Discipline_Id int primary key identity(100,1),
	Discipline_Name varchar(30) not null
);

select * from Document_Details
select * from Discipline
select * from Document_Type
select * from User_Details