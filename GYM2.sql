create database GYM2;
use GYM2;

create table MemberInfo
(
	MID  int NOT NULL IDENTITY(1,1) primary key,
	Fname varchar(50) NOT NULL,
	Lname varchar(50) NOT NULL,
	Gender varchar(20) NOT NULL,
	Email varchar(150) NOT NULL,
	Dob varchar(100) NOT NULL,
	mAddress  varchar(250) NOT NULL,
	JoinDate varchar(100) NOT NULL,
	TimeValid varchar(100) NOT NULL,
	TimeLeft varchar(50) NOT NULL
);
Alter TABLE MemberInfo
ADD Mobile varchar(150) NOT NULL;

select * from MemberInfo;