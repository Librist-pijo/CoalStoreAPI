CREATE PROCEDURE [dbo].[spCreateOrdersProducts]
	@OrderId int,
	@ProductId int,
    @Quantity int,
    @Id int out
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO OrdersProducts(OrderId,ProductId,Quantity) VALUES (@OrderId, @ProductId, @Quantity)
                SET @Id = SCOPE_IDENTITY()

		RETURN @Id
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
