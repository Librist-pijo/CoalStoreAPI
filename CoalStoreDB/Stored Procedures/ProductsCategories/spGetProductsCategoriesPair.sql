CREATE PROCEDURE [dbo].[spGetProductsCategoriesPair]
	@ProductId INT,
	@CategoryId INT
AS
BEGIN
	SELECT * FROM ProductsCategories WHERE ProductId = @ProductId AND CategoryId = @CategoryId
END

