CREATE PROCEDURE [dbo].[spCreateOrdersProducts]
	@OrderId int,
	@ProductId int,
    @Quantity int
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO OrdersProducts(OrderId,ProductId,Quantity) VALUES (@OrderId, @ProductId, @Quantity)
    END TRY
    BEGIN CATCH
        RETURN ERROR_MESSAGE() 
    END CATCH
END
