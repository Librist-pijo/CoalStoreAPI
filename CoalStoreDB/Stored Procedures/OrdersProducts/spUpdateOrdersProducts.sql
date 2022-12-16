CREATE PROCEDURE [dbo].[spUpdateOrdersProducts]
	@ProductId INT,
	@OrderId INT,
	@Quantity INT,
	@Id INT
AS
BEGIN
	UPDATE OrdersProducts
	SET
		ProductId = @ProductId,
		OrderId = @OrderId,
		Quantity = @Quantity
	WHERE
		Id = 	@Id 
END