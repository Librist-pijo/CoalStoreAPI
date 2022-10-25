CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [ShippingAddress] NVARCHAR(255) NOT NULL, 
    [ShippingDate] DATETIME2 NULL, 
    [Status] INT NOT NULL,
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([Id])
)
