alter table [dbo].[Dentists] drop column DisplayName;
alter table [dbo].[Dentists] add DisplayName nvarchar(MAX);