--CREATE DATABASE MonsterHunter;

USE MonsterHunter;

CREATE TABLE Hunters (
	HunterID int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(150) NOT NULL,
	Location varchar(100) NOT NULL
);

CREATE TABLE Orders (
	OrderID int IDENTITY(1,1) PRIMARY KEY,
	HunterID int FOREIGN KEY REFERENCES Hunters(HunterID),
	OrderDate date NOT NULL,
	DeliveryDate date
);

CREATE TABLE Products (
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	ProductName varchar(100) NOT NULL,
	UnitPrice decimal NOT NULL,
	Description varchar(MAX),
	Category varchar(30) NOT NULL,
	Rarity int NOT NULL
);

CREATE TABLE OrderDetails (
	OrderDetailsID int IDENTITY(1,1) PRIMARY KEY,
	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),
	ProductID int FOREIGN KEY REFERENCES Products(ProductID),
	Quantity int NOT NULL,
	UnitPrice decimal NOT NULL
);