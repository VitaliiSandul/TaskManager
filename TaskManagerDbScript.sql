create database TaskManagerDb;
go

use TaskManagerDb;
go

create table [AppRole]
(
	RoleId int primary key identity(1,1),
	RoleName varchar(100),
)

insert into [AppRole](RoleName)
values ('Admin'),
('User');

create table [AppUser]
(
	UserId int primary key identity(1,1),	
	FirstName varchar(100),
	LastName varchar(100),
	Email varchar(100),
	Photo varchar(250),
	Birthday Date,
	Phone varchar(15),
	"Login" varchar(100) UNIQUE,
	"Password" varchar(100),
	Token varchar(250)
);


insert into [AppUser](FirstName, LastName, Email, Birthday, Phone, "Login", "Password")
values ('Admin', 'Admin', 'admin@gmail.com', '2000-01-01', '0931234567', 'admin', 'admin'),
('Sergey', 'Yarosh', 'yarosh@gmail.com', '1986-12-22', '0951111111', 'sergyarosh', 'sergyarosh'),
('Vitalii', 'Sandul', 'vitaliisan@gmail.com', '1985-07-17', '0983331229', 'vitaliisandul', 'vitaliisandul');

create table [UserRole]
(	
	UserRoleId int primary key identity(1,1),
	UserId int foreign key REFERENCES dbo.[AppUser](UserId), 	
	RoleId int foreign key REFERENCES dbo.[AppRole](RoleId)	
);

insert into [UserRole](UserId, RoleId)
values (1, 1),
(1, 2),
(2, 2),
(3, 2);


create table [AppTask]
(
	TaskId int primary key identity(1,1),
	UserId int foreign key REFERENCES dbo.[AppUser](UserId), 	
	TaskTitle varchar(100) not null,	
	TaskDetails varchar(250) not null,
	TaskCreationDate Date,
	TaskStatus bit default 'false',	
	TaskPriority varchar(10) NOT NULL CHECK (TaskPriority IN('Low', 'Medium', 'High')) default 'Low'
);

insert into [AppTask](UserId, TaskTitle, TaskDetails, TaskCreationDate, TaskPriority)
values (2, 'Task 1', 'to do smth 1', '2020-10-02', 'Medium'),
(3, 'Task 2', 'to do smth 2', '2020-10-02', 'Low'),
(2, 'Task 3', 'to do smth 3', '2020-10-03', 'Medium'),
(3, 'Task 4', 'to do smth 4', '2020-10-02', 'Low'),
(2, 'Task 5', 'to do smth 5', '2020-10-03', 'High');


