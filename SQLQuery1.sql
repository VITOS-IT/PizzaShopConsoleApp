create database PizzaShop
use PizzaShop


create table Users(
	user_email varchar(30) primary key,
	name varchar(20),
	password varchar(30),
	address varchar(100),
	phone varchar(20)
	)
select * from Users

create table Pizza(
	pizza_number int IDENTITY(1,1) primary key,
	name varchar(20),
	prise int,
	type varchar(20)
	)

insert into Pizza(name, prise, type) values ('Margarita', 20, 'Plain')
insert into Pizza(name, prise, type) values ('Pepperoni', 25, 'Spicy')
insert into Pizza(name, prise, type) values ('4 cheese ', 15, 'Cheezy')



create table Topings(
	 Topping_number int IDENTITY(1,1) primary key,
	 name varchar(20),
	 prise int
	 )
insert into Topings(name,prise) values('Olives', 2)
insert into Topings(name,prise) values('Tomato', 5)
insert into Topings(name,prise) values('Onion ', 4)


create table Orders(
	o_id int IDENTITY(1,1) primary key,
	user_Id varchar(30) foreign key references Users(user_email),
	totall int,
	deliverharge varchar(50),
	status varchar(20)
	)

create table Orders_details(
	item_number int IDENTITY(1,1) primary key,
	o_id int foreign key references Orders(o_id),
	pizza_number int foreign key references Pizza(pizza_number)
	)

create table Order_Item_Details(
	id int IDENTITY(1,1) primary key,
	item_number int foreign key references Orders_details(item_number),
	topping_number int foreign key references Topings(Topping_number) 
	)

	
	
	drop table Order_Item_Details

	select * from Orders_details
	select * from Orders