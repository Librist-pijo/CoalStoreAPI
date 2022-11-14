CREATE PROCEDURE [dbo].[spGetProductsCategoriesByProductId]
	@ProductId INT
AS
BEGIN
	SELECT * FROM ProductsCategories WHERE ProductId = @ProductId
END

