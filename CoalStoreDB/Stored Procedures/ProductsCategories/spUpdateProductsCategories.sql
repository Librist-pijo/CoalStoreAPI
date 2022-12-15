CREATE PROCEDURE [dbo].[spUpdateProductsCategories]
	@ProductId INT,
	@CategoryId INT,
	@Id INT
AS
BEGIN
	UPDATE ProductsCategories
	SET
		ProductId = @ProductId,
		CategoryId = @CategoryId
	WHERE
		Id = @Id 
END
