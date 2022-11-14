CREATE PROCEDURE [dbo].[spGetOrdersProductsByOrderId]
	@OrderId INT
AS
BEGIN
	SELECT * FROM OrdersProducts WHERE OrderId = @OrderId
END

