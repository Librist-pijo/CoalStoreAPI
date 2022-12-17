CREATE PROCEDURE [dbo].[spGetInvoiceByOrderId]
	@orderId INT
AS
BEGIN
	SELECT * FROM Invoices WHERE OrderId = @orderId
END

