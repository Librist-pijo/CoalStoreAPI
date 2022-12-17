CREATE TABLE [dbo].[Invoices]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [PaymentMethod] NVARCHAR(255) NOT NULL, 
    [State] INT NOT NULL, 
    [Amount] MONEY NOT NULL,
    CONSTRAINT [FK_Invoices_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id])
)
