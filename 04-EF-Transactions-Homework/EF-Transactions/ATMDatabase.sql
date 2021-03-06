USE MASTER
CREATE DATABASE ATM
GO

USE ATM

CREATE TABLE CardAccounts(
	Id INT PRIMARY KEY IDENTITY,
	CardNumber NVARCHAR(10)  NOT NULL,
	CardPIN NVARCHAR(4) NOT NULL,
	CardCash MONEY NOT NULL
)
GO

INSERT INTO CardAccounts(CardNumber, CardPIN, CardCash)
				  VALUES('1122334455', '1111', 500),
						('1111334455', '2222', 400),
						('2222334455', '3333', 300),
						('3322334455', '4444', 1000),
						('4422334455', '2486', 9000),
						('5522334455', '2684', 1500),
						('6622334455', '9764', 800),
						('7722334455', '1346', 5050)
GO

USE ATM

CREATE TABLE TransactionHistory (
Id INT PRIMARY KEY IDENTITY NOT NULL,
CardNumber NVARCHAR(10) NOT NULL,
TransactionDate DATETIME NOT NULL,
Amount MONEY NOT NULL)
GO