CREATE TABLE [dbo].[Ratings] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [DentistId]     INT      NOT NULL,
    [PatientId]     INT      NOT NULL,
    [Score] float NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([DentistId]) REFERENCES [dbo].[Dentists] ([Id]),
    FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id])
);