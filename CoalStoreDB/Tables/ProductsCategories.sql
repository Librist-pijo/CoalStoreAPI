CREATE TABLE [dbo].[ProductsCategories]
(
	[ProductId] INT NOT NULL , 
    [CategoryId] INT NOT NULL, 
    PRIMARY KEY ([CategoryId],[ProductId]), 
    CONSTRAINT [FK_ProductsCategories_ToCategories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_ProductsCategories_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
