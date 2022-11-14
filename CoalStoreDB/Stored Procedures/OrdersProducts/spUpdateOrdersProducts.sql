CREATE PROCEDURE [dbo].[spUpdateOrdersProducts]
	@ProductId INT,
	@OrderId INT,
	@Quantity INT
AS
BEGIN
	UPDATE OrdersProducts
	SET
		ProductId = @ProductId,
		OrderId = @OrderId,
		Quantity = @Quantity
	WHERE
		ProductId = @ProductId AND OrderId = @OrderId
END