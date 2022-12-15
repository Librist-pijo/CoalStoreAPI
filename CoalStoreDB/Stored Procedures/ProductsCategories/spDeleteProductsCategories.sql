CREATE PROCEDURE [dbo].[spDeleteProductsCategories]
	@Id INT
AS
BEGIN
    	DELETE ProductsCategories WHERE Id = @Id
END
