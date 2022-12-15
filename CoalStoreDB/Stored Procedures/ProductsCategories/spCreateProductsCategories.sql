CREATE PROCEDURE [dbo].[spCreateProductsCategories]
	@CategoryId int,
	@ProductId int,
    @Id int out
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
        INSERT INTO ProductsCategories(CategoryId,ProductId) VALUES (@CategoryId, @ProductId)
                SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
