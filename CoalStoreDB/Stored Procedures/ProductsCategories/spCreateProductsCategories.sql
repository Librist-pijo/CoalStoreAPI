CREATE PROCEDURE [dbo].[spCreateProductsCategories]
	@CategoryId int,
	@ProductId int
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO ProductsCategories(CategoryId,ProductId) VALUES (@CategoryId, @ProductId)
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
