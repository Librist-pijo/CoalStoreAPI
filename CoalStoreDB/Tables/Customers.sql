CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL IDENTITY , 
    [Login] NVARCHAR(255) NOT NULL, 
    [Password] BINARY(64) NOT NULL, 
    [FirstName] NVARCHAR(255) NULL, 
    [LastName] NVARCHAR(255) NULL, 
    [AddressLine1] NVARCHAR(255) NULL, 
    [AddressLine2] NVARCHAR(255) NULL, 
    [PostCode] NVARCHAR(255) NULL, 
    PRIMARY KEY ([Id])
)
