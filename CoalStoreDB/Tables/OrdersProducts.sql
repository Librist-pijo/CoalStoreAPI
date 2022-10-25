CREATE TABLE [dbo].[OrdersProducts]
(
	[OrderId] INT NOT NULL , 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    PRIMARY KEY ([OrderId],[ProductId]), 
    CONSTRAINT [FK_OrdersProducts_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id]),
    CONSTRAINT [FK_OrdersProducts_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
