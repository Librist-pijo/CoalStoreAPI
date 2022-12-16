CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [ShippingAddress] NVARCHAR(255) NULL, 
    [ShippingDate] DATETIME2 NULL, 
    [State] INT NOT NULL,
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([Id])
)
