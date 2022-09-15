-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
[Id] int IDENTITY(1,1) NOT NULL,
[UserId] nvarchar(max) NOT NULL
PRIMARY KEY (Id)
);
GO
-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
[Id] int IDENTITY(1,1) NOT NULL,
[Name] nvarchar(max) NOT NULL,
[Address] nvarchar(max) NOT NULL,
[PhoneNumber] nvarchar(max) NOT NULL,
[EmailAddress] nvarchar(max) NOT NULL,
PRIMARY KEY (Id)
);
GO
-- Creating table 'Dentists'
CREATE TABLE [dbo].[Dentists] (
[Id] int IDENTITY(1,1) NOT NULL,
[FirstName] nvarchar(max) NOT NULL,
[LastName] nvarchar(max) NOT NULL,
[LocationId] int NOT NULL,
[AggregatedRating] decimal(2,1)
PRIMARY KEY (Id),
FOREIGN KEY (LocationId) REFERENCES Locations (Id)
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
[Id] int IDENTITY(1,1) NOT NULL,
[StartDateTime] DateTime NOT NULL,
[EndDateTime] DateTime NOT NULL,
[LocationId] int NOT NULL,
[DentistId] int NOT NULL,
[PatientId] int NOT NULL
PRIMARY KEY (Id)
	FOREIGN KEY (LocationId) REFERENCES Locations (Id),
	FOREIGN KEY (DentistId) REFERENCES Dentists (Id),
	FOREIGN KEY (PatientId) REFERENCES Patients (Id),
);
GO