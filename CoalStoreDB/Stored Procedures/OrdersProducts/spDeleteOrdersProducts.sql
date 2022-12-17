CREATE PROCEDURE [dbo].[spDeleteOrdersProducts]
    @orderId int 
AS
BEGIN
    	DELETE OrdersProducts WHERE OrderId = @orderId
END
