CREATE DATABASE VideoKlub2018
GO

USE VideoKlub2018
GO

CREATE TABLE Zanr
(
ZanrId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
NazivZanra nvarchar(50)  NOT NULL
);

CREATE TABLE Film(
	FilmId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NazivFilma nvarchar(50)  NOT NULL,
	Godina int NOT NULL,
	ZanrId int  NOT NULL FOREIGN KEY REFERENCES Zanr(ZanrId),
 );

CREATE TABLE Clan(
	ClanId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Ime nvarchar(50)  NOT NULL,
	Prezime nvarchar(50)  NOT NULL,
	Jmbg char(13) NOT NULL,
	Adresa nvarchar(50)  NOT NULL,
	Telefon nvarchar(50)  NOT NULL
);

CREATE TABLE Iznajmljivanje(
	IznajmljivanjeId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FilmId int NOT NULL FOREIGN KEY REFERENCES Film(FilmId),
	ClanId int NOT NULL FOREIGN KEY REFERENCES Clan(ClanId),
	DatumIznajmljivanja date NOT NULL,
	DatumVracanja date NULL,
	Cena  decimal(10, 3) NULL
	)
 
 GO
CREATE VIEW ViewIznajmljivanja
AS
SELECT iz.IznajmljivanjeId, f.NazivFilma, c.Ime, c.Prezime, iz.DatumIznajmljivanja, iz.DatumVracanja, iz.Cena
FROM  Clan as c INNER JOIN  Iznajmljivanje as iz
ON c.ClanId = iz.ClanId
INNER JOIN  Film as f ON iz.FilmId = f.FilmId