CREATE PROCEDURE [dbo].[spGetOrdersByCustomerId]
	@CustomerId INT
AS
BEGIN
	SELECT * FROM Orders WHERE CustomerId = @CustomerId
END