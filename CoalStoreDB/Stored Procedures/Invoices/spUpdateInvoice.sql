CREATE PROCEDURE [dbo].[spUpdateInvoice]
	@Id INT,
	@OrderId INT,
	@PaymentMethod NVARCHAR(255),
	@Amount MONEY,
	@Status INT
AS
BEGIN
	UPDATE Invoices
	SET
		OrderId = @OrderId,
		PaymentMethod = @PaymentMethod,
		Amount = @Amount,
		Status = @Status
	WHERE
		Id = @Id
END
