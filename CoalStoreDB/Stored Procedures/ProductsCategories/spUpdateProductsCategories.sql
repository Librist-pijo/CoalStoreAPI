CREATE PROCEDURE [dbo].[spUpdateProductsCategories]
	@ProductId INT,
	@CategoryId INT
AS
BEGIN
	UPDATE ProductsCategories
	SET
		ProductId = @ProductId,
		CategoryId = @CategoryId
	WHERE
		ProductId = @ProductId AND CategoryId = @CategoryId
END
