CREATE PROCEDURE [dbo].[spDeleteOrdersProducts]
	@ProductId int = 0,
	@OrderId int
AS
BEGIN
    	DELETE OrdersProducts WHERE ProductId = @ProductId AND OrderId = @OrderId
END
