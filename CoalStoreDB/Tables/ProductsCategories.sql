CREATE TABLE [dbo].[ProductsCategories]
(
	[ProductId] INT NOT NULL , 
    [CategoryId] INT NOT NULL, 
    [Id] INT NOT NULL IDENTITY, 
    PRIMARY KEY ([CategoryId], [ProductId], [Id]), 
    CONSTRAINT [FK_ProductsCategories_ToCategories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_ProductsCategories_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
