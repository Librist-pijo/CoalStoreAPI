CREATE PROCEDURE [dbo].[spGetProductsCategoriesById]
	@Id INT
AS
BEGIN
	SELECT * FROM ProductsCategories WHERE Id = @Id
END
