use TheBookHub;

create table LoginTable
(
	ProfilePic varbinary(max) ,
	Username varchar(250) not null primary key,
	"Name" varchar(250) not null,
	"Password" varchar (250) not null,
	Email varchar(250) not null unique,
	

);

create table NewBook
(
	Book_ID int not null primary key,
	Book_Name varchar(250) not null,
	Book_Author_Name varchar(250) not null,
	Book_Publication varchar(250) not null,
	Book_Purchase_Date varchar(250) not null,
	Book_Price varchar(250) not null,
	Book_Quantity varchar(250) not null,
	"Days" varchar(250),
	
);

create table NewStudent
(
	Enrollment_No varchar(250) not null primary key,
	Student_Name varchar(250) not null,
	Department varchar(250) not null,
	Semester varchar(250) not null,
	Contact_No varchar(250) not null,
	Email varchar(250) not null,

);

create table Issue_Book
(
	Student_Name varchar(250) not null,
	Enrollment_No varchar(250) not null,
	Department varchar(250) not null,
	Semester varchar(250) not null,
	Contact_No varchar(250) not null,
	Email varchar(250) not null,
	Book_Name varchar(250) not null,
	Book_ID int not null,
	Book_Issue_Date date not null,
	Book_Return_Date date,
);
 

insert into LoginTable values ((select * from openrowset(bulk N'C:\Users\MANI-PC\Desktop\The Book Hub\Pics\Profile Picture\1.jpg', single_blob)as T1),'mani123988','Muhammad AbdurRehman','Apple@123','ayesha100gr@gmail.com');
insert into LoginTable values ((select * from openrowset(bulk N'C:\Users\MANI-PC\Desktop\The Book Hub\Pics\Profile Picture\2.jpg', single_blob)as T2),'mani123987','Muhammad Ali','Apple@1234','muhammadabdurrehman516@gmail.com');
insert into LoginTable values ((select * from openrowset(bulk N'C:\Users\MANI-PC\Desktop\The Book Hub\Pics\Profile Picture\1.jpg', single_blob)as T3),'ali123988','Muhammad Abdullah','Apple@12345','muhammadabdurrehman517@gmail.com');
insert into LoginTable values ((select * from openrowset(bulk N'C:\Users\MANI-PC\Desktop\The Book Hub\Pics\Profile Picture\2.jpg', single_blob)as T4),'ali123987','Amna','Apple@123456','muhammadabdurrehman518@gmail.com');


select * from LoginTable;
select * from NewBook;
select * from NewStudent;
select * from Issue_Book;

delete from LoginTable;
delete from NewBook;
delete from NewStudent;
delete from Issue_Book;

drop table LoginTable;
drop table NewBook;
drop table NewStudent;
drop table Issue_Book;




select Book_Issue_Date,Book_Return_Date,datediff(dd,Book_Issue_Date,Book_Return_Date) as "Days" from Issue_Book where Book_Return_Date is not null;

select * , datediff(dd,Book_Issue_Date,Book_Return_Date) as "Days" from Issue_Book where Book_Return_Date is not null;

select *,datediff(dd,Book_Issue_Date,Book_Return_Date) as "Days",case when datediff(dd,Book_Issue_Date,Book_Return_Date) > 15 then 'Fine Rs 30' else '' end as Fine from Issue_Book where Book_Return_Date is not null;

select *,datediff(dd,Book_Issue_Date,Book_Return_Date) as Days,case when datediff(dd,Book_Issue_Date,Book_Return_Date) > 15 then 'Fine Rs 30' else '' end as Fine from Issue_Book where Book_Issue_Date between '2022-05-01' and '2022-06-30';

select count(Book_ID) from Issue_Book where Book_Return_Date is null;

select ((case when isnumeric([Book_Quantity])=1 then convert(int ,[Book_Quantity]) else 0 end)) as [Converted to Numeric] from NewBook;

select sum((case when isnumeric([Book_Quantity])=1 then convert(int ,[Book_Quantity]) else 0 end)) from NewBook;

select *,datediff(dd,Book_Issue_Date,Book_Return_Date) as Days,case when datediff(dd,Book_Issue_Date,Book_Return_Date) > 15 then 'Fine Rs 30' else '' end as Fine from Issue_Book where Book_Return_Date is not null;

select * from LoginTable where Username='' and Pass ='';

select count(Book_ID) from Issue_Book where Book_Return_Date is null;

select count(Enrollment_No) from NewStudent;

select count(Book_ID) from NewBook;

Select * from NewBook where Book_ID='';

delete from NewBook where Book_ID='';

update NewBook set Book_Name='';

select * from NewBook where Book_Name like='%';

select * from Issue_Book where Book_ID='' and Enrollment_No='' and Book_Return_Date is null;

select count(Enrollment_No) from Issue_Book where Enrollment_No='' and BooK_Return_Date is null;