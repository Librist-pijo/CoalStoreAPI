CREATE PROCEDURE [dbo].[spGetProductsCategoriesByCategoryId]
	@CategoryId INT
AS
BEGIN
	SELECT * FROM ProductsCategories WHERE CategoryId = @CategoryId
END
