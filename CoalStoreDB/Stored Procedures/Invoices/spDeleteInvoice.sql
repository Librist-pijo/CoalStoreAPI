CREATE PROCEDURE [dbo].[spDeleteInvoice]
	@orderId INT
AS
BEGIN
	DELETE Invoices WHERE OrderId = @orderId
END
