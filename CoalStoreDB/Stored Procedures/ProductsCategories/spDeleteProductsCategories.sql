CREATE PROCEDURE [dbo].[spDeleteProductsCategories]
	@ProductId int = 0,
	@CategoryId int
AS
BEGIN
    	DELETE ProductsCategories WHERE ProductId = @ProductId AND CategoryId = @CategoryId
END
